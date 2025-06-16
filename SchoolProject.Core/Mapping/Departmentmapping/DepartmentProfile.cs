using AutoMapper;

namespace SchoolProject.Core.Mapping.Departmentmapping
{
    public partial class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            GetDepartmentByIdMapping();
        }
    }
}
