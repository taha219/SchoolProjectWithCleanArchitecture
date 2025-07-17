using System.Net;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.AuthenticationUser.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Results;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Core.Feature.AuthenticationUser.Commands.Handler
{
    public class AuthenticationUserHandler : IRequestHandler<SignInCommand, ApiResponse<JWTAuthResult>>,
                                             IRequestHandler<RefreshTokenCommand, ApiResponse<JWTAuthResult>>,
                                             IRequestHandler<SendResetPasswordOtpCommand, ApiResponse<string>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAuthenticationUserService _authenticationUserService;
        private readonly IEmailsService _emailsService;
        private readonly IOTPService _otpService;
        public AuthenticationUserHandler(UserManager<AppUser> userManager,
                                         IStringLocalizer<SharedResources> stringLocalizer,
                                         SignInManager<AppUser> signInManager,
                                         IAuthenticationUserService authenticationUserService,
                                         IEmailsService emailsService,
                                         IOTPService otpService)
        {
            _userManager = userManager;
            _stringLocalizer = stringLocalizer;
            _signInManager = signInManager;
            _authenticationUserService = authenticationUserService;
            _emailsService = emailsService;
            _otpService = otpService;
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
            var identifier = request.Identifier.Trim();

            AppUser user = null;

            if (identifier.Contains("@"))
                user = await _userManager.FindByEmailAsync(identifier);
            else
                user = _userManager.Users.FirstOrDefault(u => u.PhoneNumber == identifier);

            if (user == null)
                return new ApiResponse<string> { IsSuccess = false, Message = _stringLocalizer[SharedResourcesKeys.UserNotFound] };

            var otpCode = new Random().Next(100000, 999999).ToString();

            // Send OTP
            if (identifier.Contains("@"))
            {
                var message = $"Your reset password code is: {otpCode}";
                await _emailsService.SendEmail(user.Email, message, "Reset Password Code");
            }
            else
            {
                await _otpService.SendOtpAsync($"+2{user.PhoneNumber}");
            }

            return new ApiResponse<string> { IsSuccess = false, Message = _stringLocalizer[SharedResourcesKeys.OTPSentSuccess] };
        }
    }
}
