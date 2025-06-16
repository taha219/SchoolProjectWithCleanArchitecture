using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Students.Queries.Results;

namespace SchoolProject.Core.Feature.Students.Queries.Models
{
    public class GetStudentByIdQuery : IRequest<ApiResponse<GetSingleStudentResponse>>
    {
        public int Id { get; set; }

        public GetStudentByIdQuery(int id)
        {
            Id = id;
        }
    }
}