using SchoolProject.Core.Feature.Students.Commands.Models;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.StudentMapping
{
    public partial class StudentProfile
    {
        public void EditStudentDepartmentMapping()
        {
            CreateMap<EditStudentDepartmentCommand, Student>()
                .ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.DeptId));
        }
    }
}
