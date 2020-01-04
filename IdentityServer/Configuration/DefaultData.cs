using System.Collections.Generic;
using IdentityServer4.Models;

namespace IdentityServer.Configuration
{
    public static class DefaultData
    {
        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            new IdentityResources.Phone()
        };
    }
}
