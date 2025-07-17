using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Feature.AuthenticationUser.Commands.Models
{
    public class SendResetPasswordOtpCommand : IRequest<ApiResponse<string>>
    {
        public string Identifier { get; set; } // رقم أو إيميل
    }

}
