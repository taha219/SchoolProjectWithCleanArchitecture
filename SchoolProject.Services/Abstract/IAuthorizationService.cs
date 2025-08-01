﻿using Microsoft.AspNetCore.Identity;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Requests;
using SchoolProject.Data.Results;

namespace SchoolProject.Services.Abstract
{
    public interface IAuthorizationService
    {
        public Task<string> AddRoleAsync(string roleName);
        public Task<List<IdentityRole>> GetRolesListAsync();
        public Task<IdentityRole> GetRoleByIdAsync(string roleId);
        public Task<ManageUserRolesResult> ManageUserRolesAsync(AppUser user);
        public Task<string> UpdateUserRolesAsync(UpdateUserRolesRequest request);
    }
}
