using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Authorization.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Core.Feature.Authorization.Commands.Handler
{
    public class AuthorizationHandler : IRequestHandler<AddRoleCommand, ApiResponse<string>>
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        public AuthorizationHandler(IAuthorizationService authorizationService, IStringLocalizer<SharedResources> stringLocalizer)
        {
            _authorizationService = authorizationService;
            _stringLocalizer = stringLocalizer;
        }
        public async Task<ApiResponse<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.AddRoleAsync(request.RoleName);
            if (result == "already-exists")
                return new ApiResponse<string>
                {
                    IsSuccess = false,
                    Message = _stringLocalizer[SharedResourcesKeys.RoleAlreadyExists],
                };
            else if (result == "success")
                return new ApiResponse<string>
                {
                    IsSuccess = true,
                    Message = _stringLocalizer[SharedResourcesKeys.RoleAddedSuccessfully],
                };
            else if (result == "invalid-role-name")
            {
                return new ApiResponse<string>
                {
                    IsSuccess = false,
                    Message = _stringLocalizer[SharedResourcesKeys.InvalidRoleName],
                };
            }
            else
                return new ApiResponse<string>
                {
                    IsSuccess = false,
                    Message = _stringLocalizer[SharedResourcesKeys.AddRoleFailed],
                };

        }
    }

}
