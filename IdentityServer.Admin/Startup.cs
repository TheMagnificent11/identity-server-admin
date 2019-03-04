using IdentityServer.Admin.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Admin
{
    public class Startup
    {
        private const string ApiName = "Identity Admin API";
        private const string CorsPlolicyName = "CorsPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors(CorsPlolicyName);
            app.UseAuthentication();

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", ApiName);
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCors(CorsPlolicyName);

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = Configuration["AuthServer:BaseUrl"];
                    options.RequireHttpsMetadata = false;
                    options.Audience = Configuration["AuthServer:Scope"];
                });

            services.AddAuthorization();

            services.ConfigureProblemDetails();
            services.ConfigureSwagger("v1", ApiName);
        }
    }
}
