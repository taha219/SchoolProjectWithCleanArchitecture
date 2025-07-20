namespace SchoolProject.Services.Abstract
{
    public interface IOTPService
    {
        public Task<bool> SendSmsAsync(string toPhoneNumber, string message);
        public Task<string> SendOtpAsync(string input);
    }
}
