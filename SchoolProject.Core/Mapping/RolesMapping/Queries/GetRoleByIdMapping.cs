using Microsoft.AspNetCore.Identity;
using SchoolProject.Core.Feature.Authorization.Queries.Results;

namespace SchoolProject.Core.Mapping.RolesMapping
{
    public partial class RolesProfile
    {
        public void GetRoleByIdMapping()
        {
            CreateMap<IdentityRole, GetRoleByIdResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
