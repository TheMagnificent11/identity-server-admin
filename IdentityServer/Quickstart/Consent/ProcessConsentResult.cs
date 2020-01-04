namespace IdentityServer4.Quickstart.UI
{
    public class ProcessConsentResult
    {
        public bool IsRedirect => this.RedirectUri != null;

#pragma warning disable CA1056 // Uri properties should not be strings
        public string RedirectUri { get; set; }
#pragma warning restore CA1056 // Uri properties should not be strings

        public string ClientId { get; set; }

        public bool ShowView => this.ViewModel != null;

        public ConsentViewModel ViewModel { get; set; }

        public bool HasValidationError => this.ValidationError != null;

        public string ValidationError { get; set; }
    }
}
