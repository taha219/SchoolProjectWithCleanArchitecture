using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Concrete
{
    public class StudentReposatory : GenericRepositoryAsync<Student>, IStudentReposatory
    {
        #region fields
        private readonly DbSet<Student> _students;
        #endregion

        #region constructors
        public StudentReposatory(AppDbContext context) : base(context)
        {
            _students = context.Set<Student>();
        }
        #endregion

        #region Methods
        public async Task<List<Student>> GetAllStudents()
        {
            return await _students.Include(x=>x.Department).ToListAsync();   
        }
        #endregion


    }
}
