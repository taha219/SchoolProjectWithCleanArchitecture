using System.Net;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authentication.Queries.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Core.Features.Authentication.Queries.Handles
{
    public class AuthenticationQueryHandler : IRequestHandler<AuthorizeUserQuery, ApiResponse<string>>

    {


        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IAuthenticationUserService _authenticationService;

        #endregion


        public AuthenticationQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                            IAuthenticationUserService authenticationService)
        {
            _stringLocalizer = stringLocalizer;
            _authenticationService = authenticationService;
        }

        public async Task<ApiResponse<string>> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ValidateToken(request.AccessToken);
            if (result == "NotExpired")
                return new ApiResponse<string> { IsSuccess = true, Data = result };
            return new ApiResponse<string> { IsSuccess = false, Message = _stringLocalizer[SharedResourcesKeys.TokenIsExpired], StatusCode = HttpStatusCode.Unauthorized };
        }
    }
}

