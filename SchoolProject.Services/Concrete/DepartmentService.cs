using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Services.Concrete
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentReposatory _departmentReposatory;
        public DepartmentService(IDepartmentReposatory departmentReposatory)
        {
            _departmentReposatory = departmentReposatory;
        }
    }

}
