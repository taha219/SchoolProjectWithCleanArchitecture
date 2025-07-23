using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Services.Concrete
{
    public class StudentSubjectService : IStudentSubjectService
    {
        private readonly IStudentSubjectReposatory _studentSubjectRepo;
        public StudentSubjectService(IStudentSubjectReposatory studentSubjectRepo)
        {
            _studentSubjectRepo = studentSubjectRepo;
        }
        public async Task<bool> AddStudentGradeAsync(int studentId, int subjectId, decimal grade)
        {
            var entity = new StudentSubject
            {
                StudID = studentId,
                SubID = subjectId,
                Grade = grade
            };

            await _studentSubjectRepo.AddAsync(entity);
            return true;
        }

        public async Task<bool> UpdateStudentGradeAsync(int studentId, int subjectId, decimal grade)
        {
            var existing = await _studentSubjectRepo.GetByConditionAsync(s =>
                s.StudID == studentId && s.SubID == subjectId);

            if (existing is null)
                return false;

            existing.Grade = grade;
            await _studentSubjectRepo.UpdateAsync(existing);
            return true;
        }
    }
}
