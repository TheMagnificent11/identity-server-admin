using ClaimsAuthrzr;
using IdentityServer.Admin.Authorization;
using IdentityServer.Common.Constants.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Admin.Configuration
{
    public static class Authoriation
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
            });

            services.AddSingleton<
                IAuthorizationHandler,
                ClaimValueAuthorizationHandler<ClaimValueAuthorizationRequirement>>();
        }
    }
}
