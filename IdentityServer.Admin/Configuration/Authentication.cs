using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Admin.Configuration
{
#pragma warning disable CA1724 // Type names should not match namespaces
    public static class Authentication
#pragma warning restore CA1724 // Type names should not match namespaces
    {
        public static void ConfigureAuthentication(
            this IServiceCollection services,
#pragma warning disable CA1054 // Uri parameters should not be strings
            string authServerBaseUrl,
#pragma warning restore CA1054 // Uri parameters should not be strings
            string audience)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = authServerBaseUrl;
                    options.RequireHttpsMetadata = false;
                    options.Audience = audience;
                });
        }
    }
}
