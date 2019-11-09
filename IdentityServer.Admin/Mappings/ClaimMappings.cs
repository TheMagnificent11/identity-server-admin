using AutoMapper;
using LocalClaim = IdentityServer.Admin.Models.Claim;
using SecurityClaim = System.Security.Claims.Claim;

namespace IdentityServer.Admin.Mappings
{
    public class ClaimMappings : Profile
    {
        public ClaimMappings()
        {
            this.CreateMap<SecurityClaim, LocalClaim>();
        }
    }
}
