using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Admin.Configuration
{
    public static class Authentication
    {
        public static void ConfigureAuthentication(this IServiceCollection services, string authServerBaseUrl, string audience)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = authServerBaseUrl;
                    options.RequireHttpsMetadata = false;
                    options.Audience = audience;
                });
        }
    }
}
