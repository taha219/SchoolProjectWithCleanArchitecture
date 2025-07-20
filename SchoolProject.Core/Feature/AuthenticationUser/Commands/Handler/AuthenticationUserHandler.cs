using System.Net;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.AuthenticationUser.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Results;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Core.Feature.AuthenticationUser.Commands.Handler
{
    public class AuthenticationUserHandler : IRequestHandler<SignInCommand, ApiResponse<JWTAuthResult>>,
                                             IRequestHandler<RefreshTokenCommand, ApiResponse<JWTAuthResult>>,
                                             IRequestHandler<SendResetPasswordOtpCommand, ApiResponse<string>>,
                                             IRequestHandler<VerifyOTPCommand, ApiResponse<string>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAuthenticationUserService _authenticationUserService;
        private readonly IEmailsService _emailsService;
        private readonly IOTPService _otpService;
        private readonly AppDbContext _db;
        public AuthenticationUserHandler(UserManager<AppUser> userManager,
                                         IStringLocalizer<SharedResources> stringLocalizer,
                                         SignInManager<AppUser> signInManager,
                                         IAuthenticationUserService authenticationUserService,
                                         IEmailsService emailsService,
                                         IOTPService otpService,
                                         AppDbContext db)
        {
            _userManager = userManager;
            _stringLocalizer = stringLocalizer;
            _signInManager = signInManager;
            _authenticationUserService = authenticationUserService;
            _emailsService = emailsService;
            _otpService = otpService;
            _db = db;
        }

        public async Task<ApiResponse<JWTAuthResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserNameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(request.UserNameOrEmail);
                if (user == null)
                {
                    return new ApiResponse<JWTAuthResult>
                    {
                        IsSuccess = false,
                        Message = _stringLocalizer[SharedResourcesKeys.UserNotFound]
                    };
                }
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!result.Succeeded)
            {
                return new ApiResponse<JWTAuthResult>
                {
                    IsSuccess = false,
                    Message = _stringLocalizer[SharedResourcesKeys.PasswordNotCorrect]
                };
            }
            if (!user.EmailConfirmed)
            {
                return new ApiResponse<JWTAuthResult>
                {
                    IsSuccess = false,
                    Message = _stringLocalizer[SharedResourcesKeys.EmailNotConfirmed]
                };
            }
            return new ApiResponse<JWTAuthResult>
            {
                IsSuccess = true,
                Message = _stringLocalizer[SharedResourcesKeys.SuccessSignIn],
                Data = await _authenticationUserService.GetJWTToken(user)
            };
        }

        async Task<ApiResponse<JWTAuthResult>> IRequestHandler<RefreshTokenCommand, ApiResponse<JWTAuthResult>>.Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var jwtToken = _authenticationUserService.ReadJWTToken(request.AccessToken);
            var userIdAndExpireDate = await _authenticationUserService.ValidateDetails(jwtToken, request.AccessToken, request.RefreshToken);
            switch (userIdAndExpireDate)
            {
                case ("AlgorithmIsWrong", null):
                    return new ApiResponse<JWTAuthResult>
                    {
                        IsSuccess = false,
                        Message = _stringLocalizer[SharedResourcesKeys.AlgorithmIsWrong],
                        StatusCode = HttpStatusCode.Unauthorized
                    };

                case ("TokenIsNotExpired", null):
                    return new ApiResponse<JWTAuthResult>
                    {
                        IsSuccess = false,
                        Message = _stringLocalizer[SharedResourcesKeys.TokenIsNotExpired],
                        StatusCode = HttpStatusCode.Unauthorized
                    };

                case ("RefreshTokenIsNotFound", null):
                    return new ApiResponse<JWTAuthResult>
                    {
                        IsSuccess = false,
                        Message = _stringLocalizer[SharedResourcesKeys.RefreshTokenIsNotFound],
                        StatusCode = HttpStatusCode.Unauthorized
                    };

                case ("RefreshTokenIsExpired", null):
                    return new ApiResponse<JWTAuthResult>
                    {
                        IsSuccess = false,
                        Message = _stringLocalizer[SharedResourcesKeys.RefreshTokenIsExpired],
                        StatusCode = HttpStatusCode.Unauthorized
                    };
            }

            var (userId, expiryDate) = userIdAndExpireDate;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {

                return new ApiResponse<JWTAuthResult>
                {
                    IsSuccess = false,
                    Message = _stringLocalizer[SharedResourcesKeys.NotFound]
                };
            }
            var result = await _authenticationUserService.GetRefreshToken(user, jwtToken, expiryDate, request.RefreshToken);
            return new ApiResponse<JWTAuthResult>
            {
                IsSuccess = true,
                Message = _stringLocalizer[SharedResourcesKeys.Success],
                Data = result
            };
        }
        public async Task<ApiResponse<string>> Handle(SendResetPasswordOtpCommand request, CancellationToken cancellationToken)
        {
            var result = await _otpService.SendOtpAsync(request.Identifier);
            if (result == "UserNotFound")
            {
                return new ApiResponse<string>
                {
                    IsSuccess = false,
                    Message = _stringLocalizer[SharedResourcesKeys.UserNotFound]
                };
            }
            else if (result == "FailedSendSMS")
            {
                return new ApiResponse<string>
                {
                    IsSuccess = false,
                    Message = _stringLocalizer[SharedResourcesKeys.FailedSendSMS],
                    Data = result
                };
            }
            else
            {
                return new ApiResponse<string>
                {
                    IsSuccess = true,
                    Message = _stringLocalizer[SharedResourcesKeys.CodeSentSuccessfully]
                };
            }
        }

        public async Task<ApiResponse<string>> Handle(VerifyOTPCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users
                                         .Include(x => x.Otps)
                                         .FirstOrDefaultAsync(x => x.PhoneNumber == request.Identifier || x.Email == request.Identifier);

            if (user == null)
            {
                return new ApiResponse<string>
                {
                    IsSuccess = false,
                    Message = _stringLocalizer[SharedResourcesKeys.UserNotFound]
                };
            }

            var otp = user.Otps
                .Where(o => o.Code == request.Code && o.ExpiresAt > DateTime.UtcNow && !o.IsUsed)
                .OrderByDescending(o => o.CreatedAt)
                .FirstOrDefault();

            if (otp == null)
            {
                return new ApiResponse<string>
                {
                    IsSuccess = false,
                    Message = _stringLocalizer[SharedResourcesKeys.InvalidOTPOrExpired]
                };
            }

            otp.IsUsed = true;
            await _db.SaveChangesAsync();

            return new ApiResponse<string>
            {
                IsSuccess = true,
                Message = _stringLocalizer[SharedResourcesKeys.OTPVerifiedSuccessfully],
                Data = user.Id
            };
        }
    }
}

