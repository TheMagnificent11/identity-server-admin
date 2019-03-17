using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IdentityServer.Common.Constants.Data;
using IdentityServer4.Models;

namespace IdentityServer.Admin.Controllers.Clients
{
    public class CreateClientRequest
    {
        [Required]
        [MaxLength(ClientFieldMaxLengths.ClientId)]
        public string ClientId { get; set; }

        [MaxLength(ClientFieldMaxLengths.ClientName)]
        public string ClientName { get; set; }

        [MaxLength(ClientFieldMaxLengths.Description)]
        public string Description { get; set; }

        public ICollection<string> Secrets { get; set; }

        public ICollection<string> AllowedScopes { get; set; }

        public ICollection<ClientGrantType> GrantTypes { get; set; }

        public string ClientClaimsPrefix { get; set; }

        public bool AllowOfflineAccess { get; set; }

        public int IdentityTokenLifetime { get; set; }

        public int AccessTokenLifetime { get; set; }

        public int AuthorizationCodeLifetime { get; set; }

        public int AbsoluteRefreshTokenLifetime { get; set; }

        public int SlidingRefreshTokenLifetime { get; set; }

        public int? ConsentLifetime { get; set; }

        public TokenUsage RefreshTokenUsage { get; set; }

        public bool UpdateAccessTokenClaimsOnRefresh { get; set; }

        public TokenExpiration RefreshTokenExpiration { get; set; }

        public AccessTokenType AccessTokenType { get; set; }

        public bool EnableLocalLogin { get; set; }

        public ICollection<string> IdentityProviderRestrictions { get; set; }

        public bool IncludeJwtId { get; set; }

        public int DeviceCodeLifetime { get; set; }

        public ICollection<string> RedirectUris { get; set; }

        public ICollection<string> PostLogoutRedirectUris { get; set; }
    }
}
