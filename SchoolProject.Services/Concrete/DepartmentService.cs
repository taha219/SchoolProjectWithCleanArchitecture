using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
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
        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            var department = await _departmentReposatory.GetTableNoTracking()
                               .Where(x => x.DID == id)
                               //.Include(x => x.Students)
                               .Include(x => x.DepartmentSubjects)
                               .ThenInclude(x => x.Subject)
                               .Include(x => x.Instructors)
                               .Include(x => x.Instructor)
                               .FirstOrDefaultAsync();

            return department;
        }


    }

}
