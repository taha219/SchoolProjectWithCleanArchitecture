using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Abstract
{
    public interface IStudentReposatory :IGenericRepositoryAsync<Student>
    {
        public Task<List<Student>> GetAllStudents();    
    }
}
