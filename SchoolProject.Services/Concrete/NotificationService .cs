using SchoolProject.Services.Abstract;

namespace SchoolProject.Services.Concrete
{
    public class NotificationService : INotificationService
    {
        private readonly IStudentService _studentService;
        private readonly ISignalRService _signalRService;

        public NotificationService(IStudentService studentService, ISignalRService signalRService)
        {
            _studentService = studentService;
            _signalRService = signalRService;
        }

        public async Task NotifyStudentAsync(int studentId, string subjectName, string operationType)
        {
            var student = await _studentService.GetStudentByIdAsync(studentId);
            if (student == null || string.IsNullOrEmpty(student.UserId)) return;

            var message = $"تم {operationType} الدرجة الخاصة بمادة {subjectName}";

            await _signalRService.SendNotificationAsync(student.UserId, message); // استخدم userId
        }


    }

}
