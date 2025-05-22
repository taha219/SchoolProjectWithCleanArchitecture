using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Feature.Students.Commands.Models
{
    public class EditStudentCommand : IRequest<ApiResponse<string>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public int DeptId { get; set; }
    }
}
