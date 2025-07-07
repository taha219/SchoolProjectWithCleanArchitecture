using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Services.Concrete
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        public AuthorizationService(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
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
        public async Task<ManageUserRolesResult> ManageUserRolesAsync(AppUser user)
        {
            var response = new ManageUserRolesResult();
            var rolesList = new List<UserRoles>();
            //Roles
            var roles = await _roleManager.Roles.ToListAsync();
            response.UserId = user.Id;
            foreach (var role in roles)
            {
                var userrole = new UserRoles();
                userrole.Id = role.Id;
                userrole.Name = role.Name;
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userrole.HasRole = true;
                }
                else
                {
                    userrole.HasRole = false;
                }
                rolesList.Add(userrole);
            }
            response.userRoles = rolesList;
            return response;
        }
    }
}
