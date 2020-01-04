using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Admin.Configuration
{
    [SuppressMessage("Naming", "CA1724:Type names should not match namespaces", Justification = "No namespace collission")]
    public static class Authentication
    {
        public static void ConfigureAuthentication(
            this IServiceCollection services,
            string authServerBaseUrl,
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
