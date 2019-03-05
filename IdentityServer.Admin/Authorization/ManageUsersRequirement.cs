using IdentityServer.Common.Constants.Claims;
using RequestManagement;

namespace IdentityServer.Admin.Authorization
{
    /// <summary>
    /// Management Users Authorization Requirement
    /// </summary>
    public class ManageUsersRequirement : IClaimValueAuthorizationRequirement
    {
        public string ClaimType => AdminClientClaims.ManageUsersType;

        public string ClaimValue => AdminClientClaims.ManageUsersValue;
    }
}
