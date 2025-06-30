using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Helpers;

namespace SchoolProject.Core.Feature.AuthenticationUser.Commands.Models
{
    public class RefreshTokenCommand : IRequest<ApiResponse<JWTAuthResult>>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}