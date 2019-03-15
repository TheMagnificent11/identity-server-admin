using System;
using System.Reflection;
using IdentityServer.Data;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using IdentityServer4.EntityFramework.Storage;
using IdentityServer4.EntityFramework.Stores;
using IdentityServer4.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IdentityServer.Admin.Configuration
{
    public static class Identity
    {
        public static void AddConfigurationStore(this IServiceCollection services, string connectionString)
        {
            var migrationsAssembly = typeof(ApplicationDbContext)
                .GetTypeInfo()
                .Assembly
                .GetName()
                .Name;

            services.AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = builder =>
                    builder.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
            });
        }

        private static void AddConfigurationStore(
            this IServiceCollection services,
            Action<ConfigurationStoreOptions> storeOptionsAction = null)
        {
            services.AddConfigurationDbContext<ConfigurationDbContext>(storeOptionsAction);
            services.TryAddTransient<ClientStore>();
            services.AddTransient<IClientStore, ValidatingClientStore<ClientStore>>();
            services.AddTransient<IResourceStore, ResourceStore>();
        }
    }
}
