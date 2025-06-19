using SchoolProject.Data.Entities;

namespace SchoolProject.Services.Abstract
{
    public interface IDepartmentService
    {
        public Task<Department> GetDepartmentByIdAsync(int id);
        public Task<bool> IsDepartmentIdExist(int departmentId);
    }
}
