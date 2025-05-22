using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Reposatories
{
    public class DepartmentReposatory : GenericRepositoryAsync<Department>, IDepartmentReposatory
    {
        #region fields
        private readonly DbSet<Department> _departments;
        #endregion

        #region constructors
        public DepartmentReposatory(AppDbContext context) : base(context)
        {
            _departments = context.Set<Department>();
        }
        #endregion
    }
}
