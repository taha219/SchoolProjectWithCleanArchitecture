using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Services.Abstract;
using SchoolProject.Core.Bases;


namespace SchoolProject.Services.Concrete
{
    public class StudentService :IStudentService
    {
        private readonly IStudentReposatory _studentReposatory;
        public StudentService(IStudentReposatory studentReposatory) 
        {
            _studentReposatory = studentReposatory;
        }
        public async Task<List<Student>> GetAllStudentsAsync()
        {
           return await _studentReposatory.GetAllStudents();
        }
        public async Task<Student> GetStudentByIdAsync(int id)
        {
            var student= await _studentReposatory.GetByIdAsync(id);
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

    }
}
