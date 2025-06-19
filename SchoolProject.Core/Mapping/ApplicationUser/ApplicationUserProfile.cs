using AutoMapper;

namespace SchoolProject.Core.Mapping.ApplicationUser
{
    public partial class ApplicationUserprofile : Profile
    {
        public ApplicationUserprofile()
        {
            AddUserMapping();
            UsersPaginatedList();
            GetUserByUserNameMapping();
            EditUserMapping();
        }
    }
}
