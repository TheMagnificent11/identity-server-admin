using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace IdentityServer.Admin.Client
{
    public class TokenHttpClient
    {
        public TokenHttpClient(HttpClient client, IOptions<AdminHttpClientSettings> clientSettings)
        {
            this.Client = client;
            this.ClientSettings = clientSettings.Value;
        }

        private HttpClient Client { get; }

        private AdminHttpClientSettings ClientSettings { get; }

        public async Task<string> GetTokenAsync(CancellationToken cancellationToken = default)
        {
            var request = new ClientCredentialsTokenRequest
            {
                Address = $"{this.ClientSettings.IdentityServerBaseUrl}/connect/token",
                Scope = this.ClientSettings.Scope,
                ClientId = this.ClientSettings.ClientId,
                ClientSecret = this.ClientSettings.ClientSecret
            };

            var response = await this.Client.RequestClientCredentialsTokenAsync(request, cancellationToken);

            if (response.IsError)
            {
                throw new ApiException(response.Error, (int)response.HttpStatusCode, response.ErrorDescription, null, response.Exception);
            }

            return response.AccessToken;
        }
    }
}
