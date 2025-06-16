using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Department.Queries.ResponesModels;

namespace SchoolProject.Core.Feature.Department.Queries.Models
{
    public class GetDepartmentByIdQuery : IRequest<ApiResponse<GetDepartmentByIdResponseModel>>
    {
        public int Id { get; set; }
        public int StudentPageNumber { get; set; }
        public int StudentPageSize { get; set; }

    }
}
