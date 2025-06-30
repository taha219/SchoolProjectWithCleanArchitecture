using SchoolProject.Core.Feature.ApplicationUser.Command.Models;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Mapping.ApplicationUser
{
    public partial class ApplicationUserprofile
    {
        public void AddUserMapping()
        {
            CreateMap<AddAppUserCommand, AppUser>();
        }
    }
}
