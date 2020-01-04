using System.Collections.Generic;

namespace IdentityServer4.Quickstart.UI
{
    public class ConsentInputModel
    {
        public string Button { get; set; }

        public IEnumerable<string> ScopesConsented { get; set; }

        public bool RememberConsent { get; set; }

#pragma warning disable CA1056 // Uri properties should not be strings
        public string ReturnUrl { get; set; }
#pragma warning restore CA1056 // Uri properties should not be strings
    }
}