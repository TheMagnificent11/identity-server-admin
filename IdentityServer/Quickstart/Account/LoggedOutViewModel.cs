namespace IdentityServer4.Quickstart.UI
{
    public class LoggedOutViewModel
    {
#pragma warning disable CA1056 // Uri properties should not be strings
        public string PostLogoutRedirectUri { get; set; }
#pragma warning restore CA1056 // Uri properties should not be strings

        public string ClientName { get; set; }

#pragma warning disable CA1056 // Uri properties should not be strings
        public string SignOutIframeUrl { get; set; }
#pragma warning restore CA1056 // Uri properties should not be strings

        public bool AutomaticRedirectAfterSignOut { get; set; }

        public string LogoutId { get; set; }

        public bool TriggerExternalSignout => this.ExternalAuthenticationScheme != null;

        public string ExternalAuthenticationScheme { get; set; }
    }
}