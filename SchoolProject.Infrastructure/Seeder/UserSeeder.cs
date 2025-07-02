using System.Data.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Infrastructure.Seeder
{
    public class UserSeeder
    {
        public static async Task SeedAsync(UserManager<AppUser> _userManager)
        {
            var usersCount = await _userManager.Users.CountAsync();
            if (usersCount <= 0)
            {
                var defaultuser = new AppUser()
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    PhoneNumber = "01012345789",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                await _userManager.CreateAsync(defaultuser, "A123_a");
                await _userManager.AddToRoleAsync(defaultuser, "Admin");
            }
        }
    }
}