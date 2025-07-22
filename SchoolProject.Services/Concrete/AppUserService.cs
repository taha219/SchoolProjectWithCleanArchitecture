using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Services.Abstract;

namespace SchoolProject.Services.Concrete
{
    public class AppUserService : IAppUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailsService _emailsService;
        private readonly AppDbContext _appDBContext;
        private readonly IConfiguration _configuration;
        public AppUserService(UserManager<AppUser> userManager,
                              IEmailsService emailsService,
                              AppDbContext appDBContext,
                              IConfiguration configuration)
        {
            _userManager = userManager;
            _emailsService = emailsService;
            _appDBContext = appDBContext;
            _configuration = configuration;
        }
        public async Task<(string Result, AppUser? CreatedUser)> AddUserAsync(AppUser user, string password, string role)
        {
            var trans = await _appDBContext.Database.BeginTransactionAsync();
            try
            {
                var userByName = await _userManager.FindByNameAsync(user.UserName);
                if (userByName != null) return ("UserNameExists", null);

                try
                {
                    var userByEmail = await _userManager.FindByEmailAsync(user.Email);

                }
                catch (Exception ex)
                {
                    return ("EmailExists", null);
                }
                var createResult = await _userManager.CreateAsync(user, password);
                if (!createResult.Succeeded)
                {
                    return ("CreateUserFailed", null);
                }
                await _userManager.AddToRoleAsync(user, role);


                await trans.CommitAsync();
                return ("Success", user);
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return ("Failed", null);
            }
        }


    }
}
