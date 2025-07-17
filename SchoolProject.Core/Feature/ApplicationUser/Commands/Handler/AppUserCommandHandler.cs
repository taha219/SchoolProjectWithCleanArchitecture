using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.ApplicationUser.Command.Models;
using SchoolProject.Core.Feature.ApplicationUser.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Core.Feature.ApplicationUser.Command.Handler
{
    public class AppUserCommandHandler : IRequestHandler<AddAppUserCommand, ApiResponse<string>>,
                                         IRequestHandler<EditUserCommand, ApiResponse<string>>,
                                         IRequestHandler<DeleteUserCommman, ApiResponse<string>>,
                                         IRequestHandler<ConfirmResetPasswordCommand, ApiResponse<string>>
    {

        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IAppUserService _appUserService;
        private readonly IAppUserService _userService;
        private readonly IOTPService _otpService;
        public AppUserCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                     UserManager<AppUser> userManager,
                                     IMapper mapper,
                                     IAppUserService appUserService,
                                     IOTPService otpService)
        {
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
            _mapper = mapper;
            _appUserService = appUserService;
            _otpService = otpService;
        }
        public async Task<ApiResponse<string>> Handle(AddAppUserCommand request, CancellationToken cancellationToken)
        {

            var mappedUser = _mapper.Map<AppUser>(request);
            var createResult = await _appUserService.AddUserAsync(mappedUser, request.Password, request.Role);

            switch (createResult)
            {
                case "EmailExists": return new ApiResponse<string> { IsSuccess = false, Message = _stringLocalizer[SharedResourcesKeys.UserWithExistEmailFound] };
                case "UserNameExists": return new ApiResponse<string> { IsSuccess = false, Message = _stringLocalizer[SharedResourcesKeys.UserWithExistUserNameFound] };
                case "CreateUserFailed": return new ApiResponse<string> { IsSuccess = false, Message = _stringLocalizer[SharedResourcesKeys.AddUserFailed] };
                case "Failed": return new ApiResponse<string> { IsSuccess = false, Message = _stringLocalizer[SharedResourcesKeys.TryToRegisterAgain] };
                case "Success": return new ApiResponse<string> { IsSuccess = true, Message = _stringLocalizer[SharedResourcesKeys.AddUserSuccessfully] };
                default: return new ApiResponse<string> { IsSuccess = false, Message = _stringLocalizer[SharedResourcesKeys.AddUserFailed] };
            }
        }
        public async Task<ApiResponse<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var olduser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (olduser == null)
            {
                return new ApiResponse<string> { IsSuccess = false, Message = _stringLocalizer[SharedResourcesKeys.UserNotFound] };
            }
            var userbyName = await _userManager.FindByNameAsync(request.UserName);
            var userByemail = await _userManager.FindByEmailAsync(request.Email);

            if (userbyName == null && userByemail == null)
            {
                var mappedUser = _mapper.Map(request, olduser);
                var result = await _userManager.UpdateAsync(mappedUser);
                if (!result.Succeeded)
                {
                    return new ApiResponse<string> { IsSuccess = false, Message = _stringLocalizer[SharedResourcesKeys.EditUserFailed] };
                }
                else
                {
                    return new ApiResponse<string> { IsSuccess = true, Message = _stringLocalizer[SharedResourcesKeys.EditUserSuccessfully] };
                }
            }
            else if (userbyName != null)
            {
                return new ApiResponse<string> { IsSuccess = false, Message = _stringLocalizer[SharedResourcesKeys.UserWithExistUserNameFound] };
            }
            else
            {
                return new ApiResponse<string> { IsSuccess = false, Message = _stringLocalizer[SharedResourcesKeys.UserWithExistEmailFound] };
            }
        }
        public async Task<ApiResponse<string>> Handle(DeleteUserCommman request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user == null)
            {
                return new ApiResponse<string> { IsSuccess = false, Message = _stringLocalizer[SharedResourcesKeys.UserNotFound] };
            }
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return new ApiResponse<string> { IsSuccess = false, Message = _stringLocalizer[SharedResourcesKeys.DeleteUserFailed] };
            }
            return new ApiResponse<string> { IsSuccess = true, Message = _stringLocalizer[SharedResourcesKeys.DeletedUser] };
        }

        public async Task<ApiResponse<string>> Handle(ConfirmResetPasswordCommand request, CancellationToken cancellationToken)
        {
            if (request.NewPassword != request.ConfirmPassword)
                return new ApiResponse<string> { IsSuccess = false, Message = _stringLocalizer[SharedResourcesKeys.PasswordsDoNotMatch] };

            var identifier = request.Identifier.Trim();
            AppUser user = null;

            if (identifier.Contains("@"))
                user = await _userManager.FindByEmailAsync(identifier);
            else
                user = _userManager.Users.FirstOrDefault(u => u.PhoneNumber == identifier);

            if (user == null)
                return new ApiResponse<string> { IsSuccess = false, Message = _stringLocalizer[SharedResourcesKeys.UserNotFound] };

            // ✅ تحقق من OTP
            if (!identifier.Contains("@"))
            {
                var verified = await _otpService.VerifyOtpAsync(user.PhoneNumber, request.Code);
                if (!verified)
                    return new ApiResponse<string> { IsSuccess = false, Message = _stringLocalizer[SharedResourcesKeys.InvalidCode] };
            }

            // Reset password
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);

            if (!result.Succeeded)
                return new ApiResponse<string> { IsSuccess = false, Message = result.Errors.FirstOrDefault()?.Description };

            return new ApiResponse<string> { IsSuccess = false, Message = _stringLocalizer[SharedResourcesKeys.PasswordChangedSuccessfully] };
        }
    }
}


