using System;
using System.Linq;
using System.Security.Claims;
using IdentityServer.Common.Constants.Claims;
using IdentityServer.Data;
using IdentityServer.Data.Models;
using IdentityServer4.Configuration;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Configuration
{
    public static class Database
    {
        public static void ConfigureDatabase(this IServiceCollection services, string connectionString)
        {
            var migrationsAssembly = typeof(ApplicationDbContext).Assembly.GetName().Name;

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            var builder = services
                .AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;

                    options.UserInteraction = new UserInteractionOptions
                    {
                        LogoutUrl = "/Account/Logout",
                        LoginUrl = "/Account/Login",
                        LoginReturnUrlParameter = "returnUrl"
                    };
                })
                .AddAspNetIdentity<ApplicationUser>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                });

#if DEBUG
            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();
#endif
        }

        public static void InitializeDatabase(
            this IApplicationBuilder app,
            string adminApiName,
            string clientId,
            string clientSecret)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var appContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                appContext.Database.Migrate();

                var grantContext = serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>();
                grantContext.Database.Migrate();

                var configContext = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                configContext.Database.Migrate();

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
