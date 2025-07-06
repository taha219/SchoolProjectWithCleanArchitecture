using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Authorization.Queries.Results;

namespace SchoolProject.Core.Feature.Authorization.Queries.Models
{
    public class GetRoleByIdQuery : IRequest<ApiResponse<GetRoleByIdResponse>>
    {

        public string Id { get; set; }
        public GetRoleByIdQuery(string id)
        {
            Id = id;
        }
    }
}
