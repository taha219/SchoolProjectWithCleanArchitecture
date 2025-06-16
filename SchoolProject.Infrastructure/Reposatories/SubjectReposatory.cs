using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Concrete
{

    public class SubjectReposatory : GenericRepositoryAsync<Subject>, ISubjectReposatory
    {
        private readonly DbSet<Subject> _subjects;
        public SubjectReposatory(AppDbContext context) : base(context)
        {
            _subjects = context.Set<Subject>();
        }
    }
}