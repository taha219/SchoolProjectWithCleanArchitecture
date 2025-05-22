using Microsoft.EntityFrameworkCore;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Services.Abstract;


namespace SchoolProject.Services.Concrete
{
    public class StudentService : IStudentService
    {
        private readonly IStudentReposatory _studentReposatory;
        private readonly IDepartmentReposatory _departmentReposatory;
        public StudentService(IStudentReposatory studentReposatory, IDepartmentReposatory departmentReposatory)
        {
            _studentReposatory = studentReposatory;
            _departmentReposatory = departmentReposatory;
        }
        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _studentReposatory.GetAllStudents();
        }
        public async Task<Student> GetStudentById_WithoutDepartmentDetails_Async(int id)
        {
            return await _studentReposatory.GetByIdAsync(id);
        }
        public async Task<Student> GetStudentByIdAsync(int id)
        {
            var student = await _studentReposatory.GetTableAsTracking()
                                                  .Include(x => x.Department)
                                                  .Where(x => x.StudentId.Equals(id))
                                                  .FirstOrDefaultAsync();
            return student;
        }
        public async Task<ApiResponse<string>> AddStudentAsync(Student student)
        {
            var result = await _studentReposatory
                                  .GetTableNoTracking()
                                  .FirstOrDefaultAsync(x => x.Name == student.Name);

            if (result != null)
            {
                return new ApiResponse<string>
                {
                    IsSuccess = false,
                    Message = "This student already exists."
                };
            }

            await _studentReposatory.AddAsync(student);

            return new ApiResponse<string>
            {
                IsSuccess = true,
                Message = "Student added successfully."
            };
        }

        public async Task<ApiResponse<string>> EditStudentAsync(Student student)
        {
            await _studentReposatory.UpdateAsync(student);

            return new ApiResponse<string>
            {
                IsSuccess = true,
                Message = "Student updated successfully."
            };
        }
        public async Task<ApiResponse<string>> EditStudentDepartmentAsync(Student student)
        {
            if (!student.DepartmentId.HasValue)
            {
                return new ApiResponse<string>
                {
                    IsSuccess = false,
                    Message = "Department ID is required."
                };
            }

            var existingdepartment = await _departmentReposatory.GetByIdAsync(student.DepartmentId.Value);
            if (existingdepartment == null)
            {
                return new ApiResponse<string>
                {
                    IsSuccess = false,
                    Message = "Department not found."
                };
            }

            await _studentReposatory.UpdateAsync(student);
            return new ApiResponse<string>
            {
                IsSuccess = true,
                Message = "Student Department updated successfully."
            };
        }
        public async Task<string> DeleteStudentAsync(Student student)
        {
            var trans = _studentReposatory.BeginTransaction();
            try
            {
                await _studentReposatory.DeleteAsync(student);
                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return $"Failed: {ex.Message}";
            }
        }

        public IQueryable<Student> GetStudentsListQuerable()
        {
            return _studentReposatory.GetTableAsTracking()
                 .Include(x => x.Department)
                 .AsQueryable();
        }
    }
}