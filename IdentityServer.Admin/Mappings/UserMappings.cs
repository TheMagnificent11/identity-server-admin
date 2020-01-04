using AutoMapper;
using IdentityServer.Admin.Models;
using IdentityServer.Data.Models;

namespace IdentityServer.Admin.Mappings
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            this.CreateMap<ApplicationUser, UserDetails>();
            this.CreateMap<RegistrationRequest, ApplicationUser>()
                .ForMember(
                    i => i.UserName,
                    j => j.MapFrom(k => k.Email));
        }
    }
}
