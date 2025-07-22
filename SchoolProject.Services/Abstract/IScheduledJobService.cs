namespace SchoolProject.Services.Abstract
{
    public interface IScheduledJobService
    {
        public Task NotifyInactiveUsersAsync();
        public Task DeleteExpiredOtpsAsync();
    }
}
