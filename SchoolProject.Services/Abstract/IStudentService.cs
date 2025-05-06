using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolProject.Data.Entities;

namespace SchoolProject.Services.Abstract
{
    public interface IStudentService
    {
        public Task<List<Student>> GetAllStudentsAsync();
    }
}
