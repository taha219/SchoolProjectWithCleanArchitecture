using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Requests;

namespace SchoolProject.Core.Feature.Authorization.Commands.Models
{
    public class UpdateUserRolesCommand : UpdateUserRolesRequest, IRequest<ApiResponse<string>>
    {
    }
}
