using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Reposatories
{
    public class InstructorReposatory : GenericRepositoryAsync<Instructor>, IInstructorReposatory
    {
        #region fields
        private readonly DbSet<Instructor> _instructors;
        #endregion

        #region constructors
        public InstructorReposatory(AppDbContext context) : base(context)
        {
            _instructors = context.Set<Instructor>();
        }
        #endregion
    }
}
