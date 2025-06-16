using SchoolProject.Core.Feature.Department.Queries.ResponesModels;
using SchoolProject.Data.Entities;
using static SchoolProject.Core.Feature.Department.Queries.ResponesModels.GetDepartmentByIdResponseModel;

namespace SchoolProject.Core.Mapping.Departmentmapping
{
    public partial class DepartmentProfile
    {
        public void GetDepartmentByIdMapping()
        {
            CreateMap<Department, GetDepartmentByIdResponseModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DID))
                .ForMember(dest => dest.DeptName, opt => opt.MapFrom(src => src.localize(src.DNameAr, src.DNameEn)))
                .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.localize(src.Instructor.ENameAr, src.Instructor.ENameEn)))
                .ForMember(dest => dest.SubjectList, opt => opt.MapFrom(src => src.DepartmentSubjects))
                // .ForMember(dest => dest.StudentList, opt => opt.MapFrom(src => src.Students))
                .ForMember(dest => dest.InstructorList, opt => opt.MapFrom(src => src.Instructors));

            //CreateMap<Student, StudentResponse>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.StudID))
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.localize(src.NameAr, src.NameEn)));

            CreateMap<DepartmentSubject, SubjectResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SubID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Subject.localize(src.Subject.SubjectNameAr, src.Subject.SubjectNameEn)));

            CreateMap<Instructor, InstructorResponse>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InsId))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.localize(src.ENameAr, src.ENameEn)));
        }
    }
}