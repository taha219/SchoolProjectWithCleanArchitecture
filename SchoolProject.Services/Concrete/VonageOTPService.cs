using Microsoft.Extensions.Configuration;
using SchoolProject.Services.Abstract;
using Vonage;
using Vonage.Request;
using Vonage.Verify;

namespace SchoolProject.Services.Concrete
{
    public class VonageOTPService : IOTPService
    {
        private readonly IConfiguration _config;
        private readonly VonageClient _client;

        public VonageOTPService(IConfiguration config)
        {
            _config = config;
            var credentials = Credentials.FromApiKeyAndSecret(_config["Vonage:APIkey"], _config["Vonage:APISecret"]);
            _client = new VonageClient(credentials);
        }

        // Send OTP
        public async Task SendOtpAsync(string phoneNumber)
        {
            var request = new VerifyRequest
            {
                Brand = _config["Vonage:BrandName"],
                Number = phoneNumber
            };

            var response = await _client.VerifyClient.VerifyRequestAsync(request);

            if (response.Status != "0")
                throw new Exception($"Error sending OTP: {response.ErrorText}");
        }

        // Verify OTP
        public async Task<bool> VerifyOtpAsync(string phoneNumber, string code)
        {
            // Vonage needs the requestId to check verification
            throw new NotImplementedException("Vonage requires request_id to verify the OTP, which needs to be stored after SendOtp");
        }
    }
}
