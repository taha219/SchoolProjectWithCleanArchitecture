
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Helpers;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Service.Implementations
{
    public class AuthenticationUserService : IAuthenticationUserService
    {
        #region Fields
        private readonly IConfiguration _iconfig;
        #endregion

        #region Constructors

        public AuthenticationUserService(IConfiguration iconfig)
        {
            _iconfig = iconfig; ;
        }

        #endregion

        #region Handle Functions
        public Task<string> GenJWTToken(AppUser user)
        {

            var userClaims = new List<Claim>
            {
                new Claim(nameof(UserClaimsModel.UserName), user.UserName),
                new Claim(nameof(UserClaimsModel.PhoneNumber), user.PhoneNumber),
                new Claim(nameof(UserClaimsModel.Email), user.Email)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iconfig["JWT:SecretKey"]));
            var sc = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken
            (
                issuer: _iconfig["JWT:Issuer"],
                audience: _iconfig["JWT:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: sc
            );
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return Task.FromResult(accessToken);
        }


        #endregion
    }
}