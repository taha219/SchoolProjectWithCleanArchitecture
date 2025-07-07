using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Results;

namespace SchoolProject.Core.Feature.Authorization.Queries.Models
{
    public class ManageUserRolesQuery : IRequest<ApiResponse<ManageUserRolesResult>>
    {
        public string UserId { get; set; }
        public ManageUserRolesQuery(string userid)
        {
            UserId = userid;
        }
    }
}
