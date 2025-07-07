using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Authorization.Queries.Models;
using SchoolProject.Core.Feature.Authorization.Queries.Results;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Results;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Core.Feature.Authorization.Queries.Handler
{
    public class RoleQueryHandler : IRequestHandler<GetRolesQuery, ApiResponse<List<GetRolesListResponse>>>,
                                    IRequestHandler<GetRoleByIdQuery, ApiResponse<GetRoleByIdResponse>>,
                                    IRequestHandler<ManageUserRolesQuery, ApiResponse<ManageUserRolesResult>>
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly UserManager<AppUser> _userManager;
        public RoleQueryHandler(IAuthorizationService authorizationService,
                                IMapper mapper,
                                IStringLocalizer<SharedResources> stringLocalizer,
                                UserManager<AppUser> userManager)
        {
            _authorizationService = authorizationService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
        }
        public async Task<ApiResponse<List<GetRolesListResponse>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _authorizationService.GetRolesListAsync();
            var result = _mapper.Map<List<GetRolesListResponse>>(roles);
            return new ApiResponse<List<GetRolesListResponse>>()
            {
                IsSuccess = true,
                Data = result,
                Message = _stringLocalizer[SharedResourcesKeys.GetAllRoles]
            };
        }

        public async Task<ApiResponse<GetRoleByIdResponse>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.GetRoleByIdAsync(request.Id);
            if (result == null)
            {
                return new ApiResponse<GetRoleByIdResponse>()
                {
                    IsSuccess = false,
                    Message = _stringLocalizer[SharedResourcesKeys.RoleNotFound]
                };
            }
            var mappedrole = _mapper.Map<GetRoleByIdResponse>(result);
            return new ApiResponse<GetRoleByIdResponse>()
            {
                IsSuccess = true,
                Data = mappedrole,
                Message = _stringLocalizer[SharedResourcesKeys.GetRoleById]
            };
        }
        public async Task<ApiResponse<ManageUserRolesResult>> Handle(ManageUserRolesQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
                return new ApiResponse<ManageUserRolesResult>
                {
                    IsSuccess = false,
                    Message = _stringLocalizer[SharedResourcesKeys.UserNotFound]
                };
            var result = await _authorizationService.ManageUserRolesAsync(user);
            return new ApiResponse<ManageUserRolesResult>
            {
                IsSuccess = true,
                Message = _stringLocalizer[SharedResourcesKeys.GetUserWithRoles],
                Data = result,
            };
        }
    }
}
