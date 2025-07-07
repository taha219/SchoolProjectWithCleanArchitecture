using Microsoft.AspNetCore.Identity;
using SchoolProject.Core.Feature.Authorization.Queries.Results;

namespace SchoolProject.Core.Mapping.RolesMapping
{
    public partial class RolesProfile
    {
        public void GetRolesListMapping()
        {
            CreateMap<IdentityRole, GetRolesListResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
