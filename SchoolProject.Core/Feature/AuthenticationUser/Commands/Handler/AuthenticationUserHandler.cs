using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.AuthenticationUser.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Core.Feature.AuthenticationUser.Commands.Handler
{
    public class AuthenticationUserHandler : IRequestHandler<SignInCommand, ApiResponse<string>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAuthenticationUserService _authenticationUserService;
        public AuthenticationUserHandler(UserManager<AppUser> userManager,
                                         IStringLocalizer<SharedResources> stringLocalizer,
                                         SignInManager<AppUser> signInManager,
                                         IAuthenticationUserService authenticationUserService)
        {
            _userManager = userManager;
            _stringLocalizer = stringLocalizer;
            _signInManager = signInManager;
            _authenticationUserService = authenticationUserService;
        }

        public async Task<ApiResponse<string>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserNameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(request.UserNameOrEmail);
                if (user == null)
                {
                    return new ApiResponse<string>
                    {
                        IsSuccess = false,
                        Message = _stringLocalizer[SharedResourcesKeys.UserNotFound]
                    };
                }
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
            {
                return new ApiResponse<string>
                {
                    IsSuccess = false,
                    Message = _stringLocalizer[SharedResourcesKeys.PasswordNotCorrect]
                };
            }
            return new ApiResponse<string>
            {
                IsSuccess = true,
                Message = _stringLocalizer[SharedResourcesKeys.PasswordChangedSuccessfully],
                Data = await _authenticationUserService.GenJWTToken(user)
            };
        }
    }
}
