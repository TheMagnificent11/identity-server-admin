using ClaimsAuthrzr;
using IdentityServer.Admin.Authorization;
using IdentityServer.Common.Constants.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Admin.Configuration
{
#pragma warning disable CA1724 // Type names should not match namespaces
    public static class Authorization
#pragma warning restore CA1724 // Type names should not match namespaces
    {
        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.ManageUsers, policy =>
                {
                    policy.Requirements.Add(new ClaimValueAuthorizationRequirement(
                        AdminClientClaims.ManageUsersType,
                        AdminClientClaims.ManageUsersValue));
                });

                options.AddPolicy(Policies.ManageClients, policy =>
                {
                    policy.Requirements.Add(new ClaimValueAuthorizationRequirement(
                        AdminClientClaims.ManageClientsType,
                        AdminClientClaims.ManageClientsValue));
                });
            });

            services.AddSingleton<
                IAuthorizationHandler,
                ClaimValueAuthorizationHandler<ClaimValueAuthorizationRequirement>>();
        }
    }
}
