namespace SchoolProject.Services.Abstract
{
    public interface IAuthorizationService
    {
        public Task<string> AddRoleAsync(string roleName);
    }
}
