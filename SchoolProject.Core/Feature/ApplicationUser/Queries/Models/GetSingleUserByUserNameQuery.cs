using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.ApplicationUser.Queries.Results;

namespace SchoolProject.Core.Feature.ApplicationUser.Queries.Models
{
    public class GetSingleUserByUserNameQuery : IRequest<ApiResponse<GetUserByUserNameResponse>>
    {
        public string UserName { get; set; }
        public GetSingleUserByUserNameQuery(string userName)
        {
            UserName = userName;
        }
    }
}
