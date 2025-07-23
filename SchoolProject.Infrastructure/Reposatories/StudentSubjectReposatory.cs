using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Reposatories
{
    public class StudentSubjectReposatory : GenericRepositoryAsync<StudentSubject>, IStudentSubjectReposatory
    {
        #region fields
        private readonly DbSet<StudentSubject> _studentsSubjects;
        #endregion

        public StudentSubjectReposatory(AppDbContext context) : base(context)
        {
            _studentsSubjects = context.Set<StudentSubject>();
        }
    }
}

