using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Requests;
using SchoolProject.Data.Results;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Services.Concrete
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _dbContext;
        public AuthorizationService(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, AppDbContext dbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _dbContext = dbContext;
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

        public async Task<string> UpdateUserRolesAsync(UpdateUserRolesRequest request)
        {
            var transact = await _dbContext.Database.BeginTransactionAsync();
            try
            {

                var user = await _userManager.FindByIdAsync(request.UserId);
                if (user == null) return "UserNotFound";

                var userRoles = await _userManager.GetRolesAsync(user);
                var removeOldRoles = await _userManager.RemoveFromRolesAsync(user, userRoles);
                if (!removeOldRoles.Succeeded) return "FailedToRemoveOldRoles";

                var newRoles = request.userRoles.Where(r => r.HasRole).Select(r => r.Name).ToList();
                var addNewRoles = await _userManager.AddToRolesAsync(user, newRoles);
                if (!addNewRoles.Succeeded)
                {
                    return "FailedToAddNewRoles";
                }
                else
                {
                    await transact.CommitAsync();
                    return "Success";
                }
            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();
                return "FailedToUpdateUserRoles";
            }
        }
    }
}
