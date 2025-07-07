using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Feature.ApplicationUser.Command.Models
{
    public class AddAppUserCommand : IRequest<ApiResponse<string>>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
