using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Authorization.Queries.Models;
using SchoolProject.Core.Feature.Authorization.Queries.Results;
using SchoolProject.Core.Resources;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Core.Feature.Authorization.Queries.Handler
{
    public class RoleQueryHandler : IRequestHandler<GetRolesQuery, ApiResponse<List<GetRolesListResponse>>>,
                                    IRequestHandler<GetRoleByIdQuery, ApiResponse<GetRoleByIdResponse>>
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        public RoleQueryHandler(IAuthorizationService authorizationService, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer)
        {
            _authorizationService = authorizationService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
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
    }
}
