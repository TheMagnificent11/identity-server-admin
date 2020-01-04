using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace IdentityServer.Admin.Configuration
{
    public static class SwaggerConfiguration
    {
        public static void ConfigureSwagger(this IServiceCollection services, string versionName, string title)
        {
            var apiKeyScheme = new OpenApiSecurityScheme
            {
                Description = "JWT Authorization Scheme",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            };

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(versionName, new OpenApiInfo { Title = title, Version = versionName });
                c.AddSecurityDefinition("Bearer", apiKeyScheme);
            });
        }
    }
}
