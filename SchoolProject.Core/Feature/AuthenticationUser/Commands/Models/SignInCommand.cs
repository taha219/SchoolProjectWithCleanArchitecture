using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Feature.AuthenticationUser.Commands.Models
{
    public class SignInCommand : IRequest<ApiResponse<string>>
    {
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
