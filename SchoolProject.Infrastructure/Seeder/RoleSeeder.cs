using Microsoft.AspNetCore.Identity;

namespace SchoolProject.Infrastructure.Seeder
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> _roleManager)
        {
            var rolesCount = _roleManager.Roles.Count();
            if (rolesCount <= 0)
            {

                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = "Admin"
                });
                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = "User"
                });
                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = "Manager"
                });
            }
        }

    }
}