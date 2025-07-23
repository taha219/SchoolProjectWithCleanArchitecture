using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Feature.StudentsSubjects.Commands.Models
{
    public class UpdateStudentGradeCommand : IRequest<ApiResponse<string>>
    {
        public int StudID { get; set; }
        public int SubID { get; set; }
        public decimal Grade { get; set; }
    }
}
