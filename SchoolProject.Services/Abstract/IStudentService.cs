using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Entities;

namespace SchoolProject.Services.Abstract
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllStudentsAsync();
        Task<Student> GetStudentByIdAsync(int id);
        Task<ApiResponse<string>> AddStudentAsync(Student student);


    }
}
