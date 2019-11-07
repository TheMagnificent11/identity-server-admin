using System;
using System.Reflection;
using AutoMapper;
using IdentityServer.Admin.Configuration;
using IdentityServer.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sgw.KebabCaseRouteTokens;

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

        public static void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            if (env == null)
                throw new ArgumentNullException(nameof(env));

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

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}");

                routes.MapRoute(
                    name: "single",
                    template: "{controller=Home}/{id}");
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", ApiName);
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = this.Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            services.AddConfigurationStore(connectionString);
            services.ConfigureCors(CorsPlolicyName);

            services.AddAutoMapper(GetMappingAssemblies());

            services
                .AddMvc(options =>
                {
                    options
                        .Conventions
                        .Add(new KebabCaseRouteTokenReplacementControllerModelConvention());
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddIdentity<User, Role>()
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
                typeof(User).Assembly,
                typeof(Startup).Assembly
            };
        }
    }
}
