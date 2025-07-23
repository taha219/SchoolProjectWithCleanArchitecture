namespace SchoolProject.Services.Abstract
{
    public interface INotificationService
    {
        Task NotifyStudentAsync(int studentId, string subjectName, string operationType);
    }

}
