using SchoolProject.Core.Feature.ApplicationUser.Queries.Results;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.ApplicationUser
{
    public partial class ApplicationUserprofile
    {
        public void GetUserByUserNameMapping()
        {
            CreateMap<AppUser, GetUserByUserNameResponse>();
        }
    }
}
