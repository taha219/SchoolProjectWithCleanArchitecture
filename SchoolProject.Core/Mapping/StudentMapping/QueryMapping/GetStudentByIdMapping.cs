using SchoolProject.Core.Feature.Students.Queries.Results;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.StudentMapping
{
    public partial class StudentProfile
    {
        public void GetStudentByIdMapping()
        {
            CreateMap<Student, GetSingleStudentResponse>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.localize(src.Department.DNameAr, src.Department.DNameEn)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.localize(src.NameAr, src.NameEn)))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.localize(src.AddressAr, src.AddressEn)));
        }
    }
}
