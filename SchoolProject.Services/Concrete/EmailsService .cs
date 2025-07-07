using MailKit.Net.Smtp;
using MimeKit;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Services.Concrete
{
    public class EmailsService : IEmailsService
    {

        #region Handle Functions
        public async Task<string> SendEmail(string email, string Message, string? reason)
        {
            try
            {
                //sending the Message of passwordResetLink
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 465, true);
                    client.Authenticate("to44327@gmail.com", );
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
