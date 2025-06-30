using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.ApplicationUser.Queries.Models;
using SchoolProject.Core.Feature.ApplicationUser.Queries.Results;
using SchoolProject.Core.Resources;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Feature.ApplicationUser.Queries.Handler
{
    public class AppUserQueryHandler : IRequestHandler<GetUsersPaginatedListQuery, PaginatedResult<GetUsersPaginatedListResponse>>,
                                       IRequestHandler<GetSingleUserByUserNameQuery, ApiResponse<GetUserByUserNameResponse>>

    {
        #region fields
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion

        #region ctor
        public AppUserQueryHandler(UserManager<AppUser> userManager, IMapper mapper, IStringLocalizer<SharedResources> stringLocalizer)
        {
            _userManager = userManager;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        #endregion

        #region handler
        public async Task<PaginatedResult<GetUsersPaginatedListResponse>> Handle(GetUsersPaginatedListQuery request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.AsQueryable();
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                users = users.Where(x => x.PhoneNumber.Contains(request.Search) || x.Email.Contains(request.Search) || x.UserName.Contains(request.Search));
                var paginatedListwithsearch = await _mapper.ProjectTo<GetUsersPaginatedListResponse>(users).ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return paginatedListwithsearch;
            }
            var paginatedList = await _mapper.ProjectTo<GetUsersPaginatedListResponse>(users).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }

        public async Task<ApiResponse<GetUserByUserNameResponse>> Handle(GetSingleUserByUserNameQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return new ApiResponse<GetUserByUserNameResponse>
                {
                    Message = _stringLocalizer[SharedResourcesKeys.UserNotFound],
                    IsSuccess = false
                };
            }
            var mappedUser = _mapper.Map<GetUserByUserNameResponse>(user);
            return new ApiResponse<GetUserByUserNameResponse>
            {
                Data = mappedUser,
                IsSuccess = true,
                Message = _stringLocalizer[SharedResourcesKeys.GetSingleUserByUserName]
            };
        }
        #endregion
    }
}
