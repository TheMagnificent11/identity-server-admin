﻿using System.Linq;
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
            switch (grantType)
            {
                case ClientGrantType.ClientCredentials:
                    return GrantType.ClientCredentials;

                case ClientGrantType.ResourceOwnerPassword:
                    return GrantType.ResourceOwnerPassword;

                case ClientGrantType.AuthorizationCode:
                    return GrantType.AuthorizationCode;

                case ClientGrantType.DeviceFlow:
                    return GrantType.DeviceFlow;

                case ClientGrantType.Hybrid:
                    return GrantType.Hybrid;

                case ClientGrantType.Implicit:
                    return GrantType.Implicit;

                default:
                    return string.Empty;
            }
        }
    }
}
