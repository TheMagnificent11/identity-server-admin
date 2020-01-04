using System.Reflection;
using AutoMapper;
using IdentityServer.Admin.Configuration;
using IdentityServer.Data;
using IdentityServer.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace IdentityServer.Admin
{
    public class Startup
    {
        private const string ApiName = "Identity Admin API";
        private const string CorsPlolicyName = "CorsPolicy";

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
            app.UseCors(CorsPlolicyName);
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
            var connectionString = this.Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            services.AddConfigurationStore(connectionString);
            services.ConfigureCors(CorsPlolicyName);

            services.AddAutoMapper(GetMappingAssemblies());

            services.AddControllers();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

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
