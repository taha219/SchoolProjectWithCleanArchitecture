using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Abstract
{
    public interface IStudentReposatory
    {
        public Task<List<Student>> GetAllStudents();    
    }
}
