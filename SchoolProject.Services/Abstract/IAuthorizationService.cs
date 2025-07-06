using Microsoft.AspNetCore.Identity;

namespace SchoolProject.Services.Abstract
{
    public interface IAuthorizationService
    {
        public Task<string> AddRoleAsync(string roleName);
        public Task<List<IdentityRole>> GetRolesListAsync();
        public Task<IdentityRole> GetRoleByIdAsync(string roleId);
    }
}
