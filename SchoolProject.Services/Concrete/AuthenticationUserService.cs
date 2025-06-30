using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Service.Implementations
{
    public class AuthenticationUserService : IAuthenticationUserService
    {
        #region Fields
        private readonly IConfiguration _iconfig;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly JwtSettings _JwtSettings;
        #endregion

        #region Constructors

        public AuthenticationUserService(IConfiguration iconfig, IRefreshTokenRepository refreshTokenRepository, UserManager<AppUser> userManager, JwtSettings jwtSettings)
        {
            _iconfig = iconfig; ;
            _refreshTokenRepository = refreshTokenRepository;
            _userManager = userManager;
            _JwtSettings = jwtSettings;
        }
        #endregion

        #region Handle Functions
        public async Task<JWTAuthResult> GetJWTToken(AppUser user)
        {
            var (jwtToken, accessToken) = await GenerateJWTToken(user);
            var refreshToken = GetRefreshToken(user.UserName);
            var userRefreshToken = new UserRefreshToken
            {
                AddedTime = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(20),
                IsUsed = true,
                IsRevoked = false,
                JwtId = jwtToken.Id,
                RefreshToken = refreshToken.refreshTokenString,
                Token = accessToken,
                UserId = user.Id
            };
            await _refreshTokenRepository.AddAsync(userRefreshToken);

            var response = new JWTAuthResult();
            response.refreshtoken = refreshToken;
            response.AccessToken = accessToken;
            return response;
        }
        private async Task<(JwtSecurityToken, string)> GenerateJWTToken(AppUser user)
        {
            var claims = await GetClaims(user);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iconfig["jwtSettings:SecretKey"]));
            var sc = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(claims: claims,
                                             issuer: _iconfig["jwtSettings:Issuer"],
                                             audience: _iconfig["jwtSettings:Audience"],
                                             expires: DateTime.Now.AddDays(1),
                                             signingCredentials: sc
                                             );
            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            return (token, accessToken);
        }

        private RefreshToken GetRefreshToken(string username)
        {
            var refreshToken = new RefreshToken
            {
                ExpiredAt = DateTime.Now.AddDays(20),
                UserName = username,
                refreshTokenString = GenerateRefreshToken()
            };
            return refreshToken;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            var randomNumberGenerate = RandomNumberGenerator.Create();
            randomNumberGenerate.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        public async Task<List<Claim>> GetClaims(AppUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(nameof(UserClaimsModel.PhoneNumber), user.PhoneNumber),
                new Claim(nameof(UserClaimsModel.Id), user.Id.ToString())
            };

            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);
            return claims;
        }

        public async Task<JWTAuthResult> GetRefreshToken(AppUser user, JwtSecurityToken jwtToken, DateTime? expiryDate, string refreshToken)
        {
            var (jwtSecurityToken, newToken) = await GenerateJWTToken(user);
            var response = new JWTAuthResult();
            response.AccessToken = newToken;
            var refreshTokenResult = new RefreshToken();
            refreshTokenResult.UserName = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimsModel.UserName)).Value;
            refreshTokenResult.refreshTokenString = refreshToken;
            refreshTokenResult.ExpiredAt = (DateTime)expiryDate;
            response.refreshtoken = refreshTokenResult;
            return response;

        }
        public JwtSecurityToken ReadJWTToken(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException(nameof(accessToken));
            }
            var handler = new JwtSecurityTokenHandler();
            var response = handler.ReadJwtToken(accessToken);
            return response;
        }

        public async Task<string> ValidateToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = _JwtSettings.ValidateIssuer,
                ValidIssuers = new[] { _JwtSettings.Issuer },
                ValidateIssuerSigningKey = _JwtSettings.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_iconfig["jwtSettings:SecretKey"])),
                ValidAudience = _JwtSettings.Audience,
                ValidateAudience = _JwtSettings.ValidateAudience,
                ValidateLifetime = _JwtSettings.ValidateLifeTime,
            };
            try
            {
                var validator = handler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);

                if (validator == null)
                {
                    return "InvalidToken";
                }

                return "NotExpired";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken)
        {
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                return ("AlgorithmIsWrong", null);
            }
            if (jwtToken.ValidTo > DateTime.UtcNow)
            {
                return ("TokenIsNotExpired", null);
            }

            //Get User

            var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimsModel.Id)).Value;
            var userRefreshToken = await _refreshTokenRepository.GetTableNoTracking()
                                             .FirstOrDefaultAsync(x => x.Token == accessToken &&
                                                                     x.RefreshToken == refreshToken &&
                                                                     x.UserId == userId);
            if (userRefreshToken == null)
            {
                return ("RefreshTokenIsNotFound", null);
            }

            if (userRefreshToken.ExpiryDate < DateTime.UtcNow)
            {
                userRefreshToken.IsRevoked = true;
                userRefreshToken.IsUsed = false;
                await _refreshTokenRepository.UpdateAsync(userRefreshToken);
                return ("RefreshTokenIsExpired", null);
            }
            var expirydate = userRefreshToken.ExpiryDate;
            return (userId, expirydate);
        }
        #endregion
    }
}