using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Options;

namespace IdentityServer.Admin.Client
{
    public partial class AdminHttpClient
    {
        public AdminHttpClient(
            HttpClient client,
            TokenHttpClient tokenHttpClient,
            IOptions<HttpClientSettings> clientSettings)
            : this(clientSettings.Value.ApiBaseUrl, client)
        {
            this.TokenHttpClient = tokenHttpClient;
        }

        private TokenHttpClient TokenHttpClient { get; }

#pragma warning disable IDE0060 // Remove unused parameter
        partial void PrepareRequest(HttpClient client, HttpRequestMessage request, string url)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            var token = this.TokenHttpClient.GetTokenAsync().Result;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
