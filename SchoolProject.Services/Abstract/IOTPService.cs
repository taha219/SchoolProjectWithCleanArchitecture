namespace SchoolProject.Services.Abstract
{
    public interface IOTPService
    {
        Task SendOtpAsync(string phoneNumber);
        Task<bool> VerifyOtpAsync(string phoneNumber, string code);
    }

}
