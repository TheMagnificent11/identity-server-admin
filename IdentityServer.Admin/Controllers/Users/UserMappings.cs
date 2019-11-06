using AutoMapper;
using IdentityServer.Admin.Models.Users;
using IdentityServer.Data;

namespace IdentityServer.Admin.Controllers.Users
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
