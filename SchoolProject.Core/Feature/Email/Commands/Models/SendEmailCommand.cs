using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Feature.Email.Commands.Models
{
    public class SendEmailCommand : IRequest<ApiResponse<string>>
    {
        public string Email { get; set; }
        public string Messege { get; set; }
    }
}
