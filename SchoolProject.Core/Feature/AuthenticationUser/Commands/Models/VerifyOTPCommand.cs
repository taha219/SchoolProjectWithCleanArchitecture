using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Feature.AuthenticationUser.Commands.Models
{
    public class VerifyOTPCommand : IRequest<ApiResponse<string>>
    {
        public string Identifier { get; set; }
        public string Code { get; set; }
    }
}
