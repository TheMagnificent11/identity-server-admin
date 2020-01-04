using System;
using System.Collections.Generic;

namespace IdentityServer4.Quickstart.UI
{
    public class GrantsViewModel
    {
        public IEnumerable<GrantViewModel> Grants { get; set; }
    }

    public class GrantViewModel
    {
        public string ClientId { get; set; }

        public string ClientName { get; set; }

#pragma warning disable CA1056 // Uri properties should not be strings
        public string ClientUrl { get; set; }

        public string ClientLogoUrl { get; set; }
#pragma warning restore CA1056 // Uri properties should not be strings

        public DateTime Created { get; set; }

        public DateTime? Expires { get; set; }

        public IEnumerable<string> IdentityGrantNames { get; set; }

        public IEnumerable<string> ApiGrantNames { get; set; }
    }
}