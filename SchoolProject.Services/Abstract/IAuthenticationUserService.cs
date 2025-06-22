using SchoolProject.Data.Entities;

namespace SchoolProject.Services.Abstract
{
    public interface IAuthenticationUserService
    {
        public Task<string> GenJWTToken(AppUser user);
    }
}
