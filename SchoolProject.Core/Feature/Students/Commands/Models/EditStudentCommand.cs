using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Feature.Students.Commands.Models
{
    public class EditStudentCommand : IRequest<ApiResponse<string>>
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string? AddressAr { get; set; }
        public string? AddressEn { get; set; }
        public string? Phone { get; set; }
        public int DeptId { get; set; }
    }
}
