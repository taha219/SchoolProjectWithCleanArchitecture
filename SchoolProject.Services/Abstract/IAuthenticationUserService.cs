﻿using System.IdentityModel.Tokens.Jwt;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;

namespace SchoolProject.Services.Abstract
{
    public interface IAuthenticationUserService
    {
        public Task<JWTAuthResult> GetJWTToken(AppUser user);
        public JwtSecurityToken ReadJWTToken(string accessToken);
        public Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string AccessToken, string RefreshToken);
        public Task<JWTAuthResult> GetRefreshToken(AppUser user, JwtSecurityToken jwtToken, DateTime? expiryDate, string refreshToken);
        public Task<string> ValidateToken(string AccessToken);
    }
}
