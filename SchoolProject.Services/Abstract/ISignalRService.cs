namespace SchoolProject.Services.Abstract
{
    public interface ISignalRService
    {
        public Task SendNotificationAsync(string userId, string message);
    }

}
