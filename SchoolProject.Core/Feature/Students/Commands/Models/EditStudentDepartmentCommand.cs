using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Feature.Students.Commands.Models
{
    public class EditStudentDepartmentCommand : IRequest<ApiResponse<string>>
    {
        public int Id { get; set; }
        public int DeptId { get; set; }
    }

}
