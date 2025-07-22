using AutoMapper;
using Hangfire;
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
        private readonly IEmailsService _emailsService;
        public AppUserCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                     UserManager<AppUser> userManager,
                                     IMapper mapper,
                                     IAppUserService appUserService,
                                     IOTPService otpService,
                                     IEmailsService emailsService)
        {
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
            _mapper = mapper;
            _appUserService = appUserService;
            _otpService = otpService;
            _emailsService = emailsService;
        }
        public async Task<ApiResponse<string>> Handle(AddAppUserCommand request, CancellationToken cancellationToken)
        {

            var mappedUser = _mapper.Map<AppUser>(request);
            var (result, createdUser) = await _appUserService.AddUserAsync(mappedUser, request.Password, request.Role);

            switch (result)
            {
                case "EmailExists": return new ApiResponse<string> { IsSuccess = false, Message = _stringLocalizer[SharedResourcesKeys.UserWithExistEmailFound] };
                case "UserNameExists": return new ApiResponse<string> { IsSuccess = false, Message = _stringLocalizer[SharedResourcesKeys.UserWithExistUserNameFound] };
                case "CreateUserFailed": return new ApiResponse<string> { IsSuccess = false, Message = _stringLocalizer[SharedResourcesKeys.AddUserFailed] };
                case "Failed": return new ApiResponse<string> { IsSuccess = false, Message = _stringLocalizer[SharedResourcesKeys.TryToRegisterAgain] };
                case "Success":
                    if (createdUser is not null)
                    {
                        BackgroundJob.Enqueue(() => _emailsService.SendEmail(createdUser.Email, "Welcome To Our System , Thanks For Registering", "Welcoming"));
                        BackgroundJob.Schedule<IEmailsService>(x => x.SendConfirmEmail(createdUser), TimeSpan.FromMinutes(2));
                    }
                    return new ApiResponse<string> { IsSuccess = true, Message = _stringLocalizer[SharedResourcesKeys.AddUserSuccessfully] };
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
            var user = await _userManager.Users
             .Include(u => u.Otps)
             .FirstOrDefaultAsync(u => u.Email == request.Identifier || u.PhoneNumber == request.Identifier);

            if (user == null)
                return new ApiResponse<string> { IsSuccess = false, Message = _stringLocalizer[SharedResourcesKeys.UserNotFound] };

            var lastUsedOtp = user.Otps
                .Where(o => o.IsUsed)
                .OrderByDescending(o => o.CreatedAt)
                .FirstOrDefault();

            if (lastUsedOtp == null || lastUsedOtp.ExpiresAt < DateTime.Now)
                return new ApiResponse<string> { IsSuccess = false, Message = _stringLocalizer[SharedResourcesKeys.OTPConfirmationRequired] };

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, request.NewPassword);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new ApiResponse<string> { IsSuccess = false, Message = errors };
            }

            return new ApiResponse<string> { IsSuccess = false, Message = _stringLocalizer[SharedResourcesKeys.PasswordChangedSuccessfully] };
        }
    }
}


