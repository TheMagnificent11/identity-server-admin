using System;
using AutoMapper;
using IdentityServer.Admin.Models;
using IdentityServer.Data.Models;

namespace IdentityServer.Admin.Mappings
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            this.CreateMap<ApplicationUser, UserDetails>()
                .ForMember(
                    i => i.Id,
                    j => j.MapFrom(k => new Guid(k.Id)));

            this.CreateMap<RegistrationRequest, ApplicationUser>()
                .ForMember(
                    i => i.UserName,
                    j => j.MapFrom(k => k.Email));
        }
    }
}
