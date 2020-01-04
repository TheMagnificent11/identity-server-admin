using System.Collections.Generic;

namespace IdentityServer4.Quickstart.UI
{
    public class ConsentViewModel : ConsentInputModel
    {
        public string ClientName { get; set; }

#pragma warning disable CA1056 // Uri properties should not be strings
        public string ClientUrl { get; set; }

        public string ClientLogoUrl { get; set; }
#pragma warning restore CA1056 // Uri properties should not be strings

        public bool AllowRememberConsent { get; set; }

        public IEnumerable<ScopeViewModel> IdentityScopes { get; set; }

        public IEnumerable<ScopeViewModel> ResourceScopes { get; set; }
    }
}
