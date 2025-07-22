using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Services.Abstract
{
    public interface IEmailsService
    {
        public Task<string> SendEmail(string email, string Message, string? reason);
        public void SendConfirmEmail(AppUser user);
    }
}
