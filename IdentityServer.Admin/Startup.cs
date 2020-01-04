using System.Reflection;
using AutoMapper;
using IdentityServer.Admin.Configuration;
using IdentityServer.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer.Admin
{
    public class Startup
    {
        private const string ApiName = "Identity Admin API";

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureDatabase(this.Configuration.GetConnectionString("DefaultConnection"));

            services.AddAutoMapper(GetMappingAssemblies());

            services.AddControllers();

            services.ConfigureAuthentication(
                this.Configuration["AuthServer:BaseUrl"],
                this.Configuration["AuthServer:Audience"]);

            services.ConfigureAuthorization();

            services.ConfigureProblemDetails();

            services.ConfigureSwagger("v1", ApiName);
        }

        private static Assembly[] GetMappingAssemblies()
        {
            return new Assembly[]
            {
                typeof(ApplicationUser).Assembly,
                typeof(Startup).Assembly
            };
        }
    }
}
