using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SchoolProject.Services.Hubs;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestNotificationController : ControllerBase
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public TestNotificationController(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("send")]
        public async Task<IActionResult> SendNotification([FromQuery] string message)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", message);
            return Ok("Notification sent");
        }
    }
}
