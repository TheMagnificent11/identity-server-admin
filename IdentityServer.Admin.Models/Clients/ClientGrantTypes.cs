using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Admin.Models.Clients
{
    public enum ClientGrantType
    {
        ClientCredentials,
        ResourceOwnerPassword,
        AuthorizationCode,
        Implicit,
        Hybrid,
        DeviceFlow
    }
}
