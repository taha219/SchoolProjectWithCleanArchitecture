using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Results;

namespace SchoolProject.Core.Feature.AuthenticationUser.Commands.Models
{
    public class SignInCommand : IRequest<ApiResponse<JWTAuthResult>>
    {
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
