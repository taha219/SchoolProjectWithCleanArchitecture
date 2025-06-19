using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.ApplicationUser.Command.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Feature.ApplicationUser.Command.Handler
{
    public class AppUserCommandHandler : IRequestHandler<AddAppUserCommand, ApiResponse<string>>
    {

        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public AppUserCommandHandler(IStringLocalizer<SharedResources> stringLocalizer, UserManager<AppUser> userManager, IMapper mapper)
        {
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<ApiResponse<string>> Handle(AddAppUserCommand request, CancellationToken cancellationToken)
        {
            var userbyName = await _userManager.FindByNameAsync(request.UserName);
            if (userbyName != null)
            {
                return new ApiResponse<string> { IsSuccess = false, Message = _stringLocalizer[SharedResourcesKeys.UserWithExistUserNameFound] };
            }
            try
            {
                var userByMail = await _userManager.FindByEmailAsync(request.Email);
            }
            catch (Exception ex)
            {
                return new ApiResponse<string>
                {
                    IsSuccess = false,
                    Message = _stringLocalizer[SharedResourcesKeys.UserWithExistEmailFound],
                };
            }
            var mappedUser = _mapper.Map<AppUser>(request);
            IdentityResult isSuccess = await _userManager.CreateAsync(mappedUser, request.Password);
            if (!isSuccess.Succeeded)
            {
                return new ApiResponse<string> { IsSuccess = false, Message = _stringLocalizer[SharedResourcesKeys.AddUserFailed] };
            }
            return new ApiResponse<string> { IsSuccess = true, Message = _stringLocalizer[SharedResourcesKeys.AddUserSuccessfully] };
        }
    }
}
