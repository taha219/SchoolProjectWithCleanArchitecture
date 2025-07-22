using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Services.Abstract
{
    public interface IAppUserService
    {
        public Task<(string Result, AppUser? CreatedUser)> AddUserAsync(AppUser user, string password, string role);
    }
}
