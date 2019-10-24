using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Admin.Client
{
    public static class AdminHttpClientRegistration
    {
        public static void RegisterAdminHttpClient(
            this IServiceCollection services,
            string apiBaseUrl,
            string identityServerBaseUrl,
            string scope,
            string clientId,
            string clientSecret)
        {
            services.Configure<AdminHttpClientSettings>(i => new AdminHttpClientSettings
            {
                ApiBaseUrl = apiBaseUrl,
                IdentityServerBaseUrl = identityServerBaseUrl,
                Scope = scope,
                ClientId = clientId,
                ClientSecret = clientSecret
            });

            services.AddHttpClient<TokenHttpClient>();

            services.AddHttpClient<AdminHttpClient>();
        }
    }
}
