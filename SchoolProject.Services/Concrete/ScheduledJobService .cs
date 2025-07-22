using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Services.Concrete
{
    public class ScheduledJobService : IScheduledJobService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailsService _emailService;
        private readonly IConfiguration _configuration;

        public ScheduledJobService(
            AppDbContext context,
            UserManager<AppUser> userManager,
            IEmailsService emailService,
            IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task DeleteExpiredOtpsAsync()
        {
            var now = DateTime.Now;
            var expiredOtps = _context.UserOtps
                .Where(o => o.ExpiresAt <= now && !o.IsUsed);

            _context.UserOtps.RemoveRange(expiredOtps);
            await _context.SaveChangesAsync();
        }

        public async Task NotifyInactiveUsersAsync()
        {
            var threshold = DateTime.Now.AddDays(-3);
            var inactiveUsers = _userManager.Users
                .Where(u => !u.EmailConfirmed && u.CreatedAt <= threshold)
                .ToList();

            foreach (var user in inactiveUsers)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationUrl = $"{_configuration["AppUrl"]}/Api/Authentication/ConfirmEmail?userId={user.Id}&code={Uri.EscapeDataString(code)}";
                var body = $"🔔 Please confirm your email: <a href='{confirmationUrl}'>Click here</a>";
                await _emailService.SendEmail(user.Email, body, "Reminder: Confirm Your Email");
            }
        }
    }
}