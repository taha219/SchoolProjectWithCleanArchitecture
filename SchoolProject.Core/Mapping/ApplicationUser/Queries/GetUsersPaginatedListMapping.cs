using SchoolProject.Core.Feature.ApplicationUser.Queries.Results;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Mapping.ApplicationUser
{
    public partial class ApplicationUserprofile
    {
        public void UsersPaginatedList()
        {
            CreateMap<AppUser, GetUsersPaginatedListResponse>();
        }
    }
}
