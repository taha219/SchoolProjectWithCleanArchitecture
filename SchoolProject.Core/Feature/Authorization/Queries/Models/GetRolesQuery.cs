using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Authorization.Queries.Results;

namespace SchoolProject.Core.Feature.Authorization.Queries.Models
{
    public class GetRolesQuery : IRequest<ApiResponse<List<GetRolesListResponse>>>
    {
    }
}
