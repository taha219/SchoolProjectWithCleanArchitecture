using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Services.Concrete
{
    public class StudentService :IStudentService
    {
        private readonly IStudentReposatory _studentReposatory;
        public StudentService(IStudentReposatory studentReposatory) 
        {
            _studentReposatory = studentReposatory;
        }
        public async Task<List<Student>> GetAllStudents()
        {
           return await _studentReposatory.GetAllStudents();
        }
    }
}
