using MediatR;
using SchoolProject.Core.Feature.ApplicationUser.Queries.Results;
using SchoolProject.Core.Wrappers;

namespace SchoolProject.Core.Feature.ApplicationUser.Queries.Models
{
    public class GetUsersPaginatedListQuery : IRequest<PaginatedResult<GetUsersPaginatedListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Search { get; set; }
    }
}
