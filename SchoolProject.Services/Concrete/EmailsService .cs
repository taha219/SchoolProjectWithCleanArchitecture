using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MimeKit;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Services.Concrete
{
    public class EmailsService : IEmailsService
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        public EmailsService(UserManager<AppUser> userManager,
                              IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        #region Handle Functions
        public async void SendConfirmEmail(AppUser user)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var confirmationUrl = $"{_configuration["AppUrl"]}/Api/Authentication/ConfirmEmail?userId={user.Id}&code={Uri.EscapeDataString(code)}";

            var message = $"To Confirm Email Click Link: <a href='{confirmationUrl}'>Link Of Confirmation</a>";

            await SendEmail(user.Email, message, "Confirm Email");
        }
        public async Task<string> SendEmail(string email, string Message, string? reason)
        {
            try
            {
                //sending the Message of passwordResetLink
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 465, true);
                    client.Authenticate("to44327@gmail.com", "dotl odqx idyu dncb");
                    var bodybuilder = new BodyBuilder
                    {
                        HtmlBody = $"{Message}",
                        TextBody = "wellcome",
                    };
                    var message = new MimeMessage
                    {
                        Body = bodybuilder.ToMessageBody()
                    };
                    message.From.Add(new MailboxAddress("Tech Team", "to44327@gmail.com"));
                    message.To.Add(new MailboxAddress("testing", email));
                    message.Subject = reason == null ? "No Submitted" : reason;
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
                //end of sending email
                return "Success";
            }
            catch (Exception ex)
            {
                return "Failed";
            }
        }

        #endregion
    }
}
