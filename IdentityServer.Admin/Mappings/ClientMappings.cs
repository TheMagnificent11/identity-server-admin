using System.Linq;
using AutoMapper;
using IdentityServer.Admin.Models;
using IdentityServer4.Models;

namespace IdentityServer.Admin.Mappings
{
    public sealed class ClientMappings : Profile
    {
        public ClientMappings()
        {
            this.CreateMap<CreateClientRequest, Client>()
                .ForMember(
                    i => i.ClientSecrets,
                    j => j.MapFrom(k => k.Secrets.Select(l => new Secret(l.Sha256(), null))))
                .ForMember(
                    i => i.AllowedGrantTypes,
                    j => j.MapFrom(k => k.GrantTypes.Select(l => MapGrantType(l))));
        }

        private static string MapGrantType(ClientGrantType grantType)
        {
            return grantType switch
            {
                ClientGrantType.ClientCredentials => GrantType.ClientCredentials,
                ClientGrantType.ResourceOwnerPassword => GrantType.ResourceOwnerPassword,
                ClientGrantType.AuthorizationCode => GrantType.AuthorizationCode,
                ClientGrantType.DeviceFlow => GrantType.DeviceFlow,
                ClientGrantType.Hybrid => GrantType.Hybrid,
                ClientGrantType.Implicit => GrantType.Implicit,
                _ => string.Empty,
            };
        }
    }
}
