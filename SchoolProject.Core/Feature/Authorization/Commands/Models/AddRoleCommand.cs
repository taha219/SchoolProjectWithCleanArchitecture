using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Feature.Authorization.Commands.Models
{
    public class AddRoleCommand : IRequest<ApiResponse<string>>
    {
        public string RoleName { get; set; }

    }
}
