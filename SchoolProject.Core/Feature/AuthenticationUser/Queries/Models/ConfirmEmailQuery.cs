using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Feature.AuthenticationUser.Queries.Models
{
    public class ConfirmEmailQuery : IRequest<ApiResponse<string>>
    {
        public string UserId { get; set; }
        public string Code { get; set; }
    }
}
