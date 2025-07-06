using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Services.Concrete
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public AuthorizationService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<string> AddRoleAsync(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                return "invalid-role-name";

            if (await _roleManager.RoleExistsAsync(roleName))
                return "already-exists";

            var identityRole = new IdentityRole();
            identityRole.Name = roleName;
            var result = await _roleManager.CreateAsync(identityRole);
            return result.Succeeded ? "success" : "failed";
        }

        public async Task<List<IdentityRole>> GetRolesListAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }
        public async Task<IdentityRole> GetRoleByIdAsync(string roleId)
        {
            return await _roleManager.FindByIdAsync(roleId);
        }
    }
}
