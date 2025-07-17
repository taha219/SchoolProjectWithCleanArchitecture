using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SchoolProject.Data.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            UserRefreshTokens = new HashSet<UserRefreshToken>();
        }
        [InverseProperty(nameof(UserRefreshToken.user))]
        public virtual ICollection<UserRefreshToken> UserRefreshTokens { get; set; }
        public virtual ICollection<UserOTP> Otps { get; set; }
    }
}
