using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Admin.Client
{
    public class HttpClientSettings
    {
        public string IdentityServerBaseUrl { get; set; }

        public string ApiBaseUrl { get; set; }

        public string Scope { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }
    }
}
