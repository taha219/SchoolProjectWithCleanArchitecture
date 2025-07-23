namespace SchoolProject.Services.Abstract
{
    public interface IStudentSubjectService
    {
        Task<bool> AddStudentGradeAsync(int studentId, int subjectId, decimal grade);
        Task<bool> UpdateStudentGradeAsync(int studentId, int subjectId, decimal grade);
    }
}
