using AutoMapper;
using IdentityServer.Admin.Models;
using IdentityServer.Data;

namespace IdentityServer.Admin.Mappings
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            this.CreateMap<User, UserDetails>();
            this.CreateMap<RegistrationRequest, User>()
                .ForMember(
                    i => i.UserName,
                    j => j.MapFrom(k => k.Email));
        }
    }
}
