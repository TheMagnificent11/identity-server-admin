using System.Linq;
using System.Security.Claims;
using IdentityServer.Common.Constants.Claims;
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
                var appContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                appContext.Database.Migrate();

                var grantContext = serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>();
                grantContext.Database.Migrate();
#endif

                var configContext = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
#if DEBUG
                configContext.Database.Migrate();
#endif

                SeedAdminClient(adminApiName, clientId, clientSecret, configContext);
            }
        }

        private static void SeedAdminClient(string adminApiName, string clientId, string clientSecret, ConfigurationDbContext configContext)
        {
            if (!configContext.IdentityResources.Any())
            {
                foreach (var resource in DefaultData.IdentityResources)
                {
                    configContext.IdentityResources.Add(resource.ToEntity());
                }
            }

            if (!configContext.ApiResources.Any())
            {
                var apiResource = new ApiResource(adminApiName, "Identity Server Admin");
                configContext.ApiResources.Add(apiResource.ToEntity());
            }

            if (!configContext.Clients.Any())
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
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    Claims =
                        {
                            new Claim(AdminClientClaims.ManageUsersType, AdminClientClaims.ManageUsersValue),
                            new Claim(AdminClientClaims.ManageClientsType, AdminClientClaims.ManageClientsValue)
                        },
                    ClientClaimsPrefix = null
                };

                configContext.Clients.Add(adminClient.ToEntity());
            }

            configContext.SaveChanges();
        }
    }
}
