using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Authentication.Queries.Models
{
    public class AuthorizeUserQuery : IRequest<ApiResponse<string>>
    {
        public string AccessToken { get; set; }
    }
}
