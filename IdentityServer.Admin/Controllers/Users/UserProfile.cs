using AutoMapper;
using IdentityServer.Data;

namespace IdentityServer.Admin.Controllers.Users
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            this.CreateMap<User, UserDetails>();
        }
    }
}
