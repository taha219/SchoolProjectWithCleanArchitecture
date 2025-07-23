using Microsoft.AspNetCore.SignalR;
using SchoolProject.Services.Abstract;
using SchoolProject.Services.Hubs;

namespace SchoolProject.Services.Concrete
{
    public class SignalRService : ISignalRService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public SignalRService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task SendNotificationAsync(string userId, string message)
        {
            return _hubContext.Clients.User(userId).SendAsync("ReceiveNotification", message);
        }

    }

}
