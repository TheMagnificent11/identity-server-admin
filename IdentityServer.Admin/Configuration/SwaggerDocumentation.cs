using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace IdentityServer.Admin.Configuration
{
    public static class SwaggerDocumentation
    {
        public static void ConfigureSwagger(this IServiceCollection services, string versionName, string title)
        {
            services.AddSwaggerDocument(options =>
            {
                options.Title = title;
                options.Version = versionName;
            });
        }
    }
}
