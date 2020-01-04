using System;
using System.Collections.Generic;
using System.Text;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;

namespace IdentityServer4.Quickstart.UI
{
    public class DiagnosticsViewModel
    {
        public DiagnosticsViewModel(AuthenticateResult result)
        {
            this.AuthenticateResult = result ?? throw new ArgumentNullException(nameof(result));

            if (result?.Properties?.Items != null && result.Properties.Items.ContainsKey("client_list"))
            {
                var encoded = result.Properties.Items["client_list"];
                var bytes = Base64Url.Decode(encoded);
                var value = Encoding.UTF8.GetString(bytes);

                this.Clients = JsonConvert.DeserializeObject<string[]>(value);
            }
        }

        public AuthenticateResult AuthenticateResult { get; }

        public IEnumerable<string> Clients { get; } = new List<string>();
    }
}