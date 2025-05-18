using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Abstract
{
    public interface IStudentReposatory : IGenericRepositoryAsync<Student>
    {
        public Task<List<Student>> GetAllStudents();

    }
}
