namespace IdentityServer.Admin.Client
{
    public class AdminHttpClientSettings
    {
        public string IdentityServerBaseUrl { get; set; }

        public string ApiBaseUrl { get; set; }

        public string Scope { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }
    }
}
