using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Feature.ApplicationUser.Commands.Models
{
    public class EditUserCommand : IRequest<ApiResponse<string>>
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
