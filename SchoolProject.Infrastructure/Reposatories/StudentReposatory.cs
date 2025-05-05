using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstract;

namespace SchoolProject.Infrastructure.Concrete
{
    public class StudentReposatory : IStudentReposatory
    {


        #region
        private readonly DbContext _context;
        #endregion

        #region constructors
        public StudentReposatory(DbContext context)
        {
            _context= context;
        }
        #endregion

        #region Methods
        public async Task<List<Student>> GetAllStudents()
        {
            return await _context.Set<Student>().ToListAsync();   
        }
        #endregion
    }
}
