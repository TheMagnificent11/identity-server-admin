using System.Linq;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Configuration
{
    public static class Database
    {
        public static void InitializeDatabase(
            this IApplicationBuilder app,
            string adminApiName,
            string clientId,
            string clientSecret)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
#if DEBUG
                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in DefaultData.IdentityResources)
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                }

                if (!context.ApiResources.Any())
                {
                    var apiResource = new ApiResource(adminApiName, "Identity Server Admin");
                    context.ApiResources.Add(apiResource.ToEntity());
                }

                if (!context.Clients.Any())
                {
                    var adminClient = new Client
                    {
                        ClientName = "Identity Server Admin",
                        ClientId = clientId,
                        ClientSecrets =
                        {
                            new Secret(clientSecret.Sha256())
                        },
                        AllowedScopes =
                        {
                            adminApiName
                        },
                        AllowedGrantTypes = GrantTypes.ClientCredentials
                    };

                    context.Clients.Add(adminClient.ToEntity());
                }

                context.SaveChanges();
#endif
            }
        }
    }
}
