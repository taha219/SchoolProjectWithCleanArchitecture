﻿using SchoolProject.Core.Feature.ApplicationUser.Commands.Models;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Mapping.ApplicationUser
{
    public partial class ApplicationUserprofile
    {
        public void EditUserMapping()
        {
            CreateMap<EditUserCommand, AppUser>();
        }
    }
}
