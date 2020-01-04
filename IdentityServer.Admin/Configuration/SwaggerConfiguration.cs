using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace IdentityServer.Admin.Configuration
{
    public static class SwaggerConfiguration
    {
        public static void ConfigureSwagger(this IApplicationBuilder app, string versionName, string title)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{versionName}/swagger.json", title);
            });
        }

        public static void ConfigureSwagger(this IServiceCollection services, string versionName, string title)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(versionName, new Info { Title = title, Version = versionName });
            });
        }
    }
}