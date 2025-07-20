using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Services.Abstract;
using Vonage;
using Vonage.Messaging;
using Vonage.Request;
namespace SchoolProject.Services.Concrete
{
    public class OTPService : IOTPService
    {
        private readonly IConfiguration _iconfig;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailsService _emailService;
        private readonly AppDbContext _context;

        public OTPService(IConfiguration iconfig, UserManager<AppUser> userManager, IEmailsService emailService, AppDbContext context)
        {
            _iconfig = iconfig;
            _userManager = userManager;
            _emailService = emailService;
            _context = context;
        }

        public async Task<string> SendOtpAsync(string input)
        {
            AppUser user = null;

            if (input.Contains("@"))
                user = await _userManager.FindByEmailAsync(input);
            else
                user = _userManager.Users.FirstOrDefault(u => u.PhoneNumber == input);
            if (user == null)
                return "UserNotFound";
            var code = new Random().Next(100000, 999999).ToString();
            var otp = new UserOTP
            {
                UserId = user.Id,
                PhoneNumber = user.PhoneNumber,
                Code = code,
                RequestId = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.Now,
                ExpiresAt = DateTime.Now.AddMinutes(5)
            };

            _context.UserOtps.Add(otp);
            try
            {
                _context.UserOtps.Add(otp);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var inner = ex.InnerException?.Message ?? ex.Message;
                return $"DatabaseError: {inner}";
            }
            string message = $"Your OTP code is: {code}";

            if (input.Contains("@"))
            {
                await _emailService.SendEmail(user.Email, "Reset Password OTP", message);
            }
            else
            {
                bool smsSent = await SendSmsAsync(user.PhoneNumber, message);
                if (!smsSent)
                    return "FailedSendSMS";
            }

            return "OTPSentSuccessfully";
        }

        public async Task<bool> SendSmsAsync(string toPhoneNumber, string message)
        {
            var credentials = Credentials.FromApiKeyAndSecret(_iconfig["Vonage:APIkey"], _iconfig["Vonage:APISecret"]);
            var client = new VonageClient(credentials);

            var response = await client.SmsClient.SendAnSmsAsync(new SendSmsRequest
            {
                To = $"+2{toPhoneNumber}",
                From = _iconfig["Vonage:BrandName"],
                Text = message
            });

            return response.Messages[0].Status == "0"; // "0" معناها success
        }
    }
}

