using SchoolProject.Core.Bases;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Helpers;

namespace SchoolProject.Services.Abstract
{
    public interface IStudentService
    {
        public Task<List<Student>> GetAllStudentsAsync();
        public Task<Student> GetStudentById_WithoutDepartmentDetails_Async(int id);
        public Task<Student> GetStudentByIdAsync(int id);
        public Task<ApiResponse<string>> AddStudentAsync(Student student);
        public Task<ApiResponse<string>> EditStudentAsync(Student student);
        public Task<ApiResponse<string>> EditStudentDepartmentAsync(Student student);
        public Task<string> DeleteStudentAsync(Student student);

        public IQueryable<Student> GetStudentsListQuerable();
        public IQueryable<Student> GetStudentsByDepartmentQuerable(int id);
        public IQueryable<Student> FilterStudentPaginatedQuerable(StudentOrderingEnum order, string search);


    }
}
