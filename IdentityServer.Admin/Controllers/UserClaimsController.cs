using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IdentityServer.Admin.Authorization;
using IdentityServer.Common.Constants;
using IdentityServer.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LocalClaim = IdentityServer.Admin.Models.Claim;
using SecurityClaim = System.Security.Claims.Claim;

namespace IdentityServer.Admin.Controllers
{
    [ApiController]
    [Route("user-claims")]
    [Authorize(Policy = Policies.ManageUsers)]
    public sealed class UserClaimsController : BaseUserController
    {
        public UserClaimsController(UserManager<ApplicationUser> userManager, IMapper mapper)
            : base(userManager)
        {
            this.Mapper = mapper;
        }

        private IMapper Mapper { get; }

        [HttpGet("{email}")]
        [Produces(ContentTypes.ApplicationJson)]
        [ProducesResponseType(200, Type = typeof(IList<LocalClaim>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get([FromRoute]string email)
        {
            if (email == null)
                throw new ArgumentNullException(nameof(email));

            var user = await this.UserManager.FindByEmailAsync(email);
            if (user == null)
                return this.NotFound();

            var claims = await this.UserManager.GetClaimsAsync(user) ?? new List<SecurityClaim>();

            return this.Ok(this.Mapper.Map<List<LocalClaim>>(claims));
        }

        [HttpPut("{email}")]
        [Consumes(ContentTypes.ApplicationJson)]
        [Produces(ContentTypes.ApplicationJson)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Put(
            [FromRoute]string email,
            [FromBody]LocalClaim claim)
        {
            if (email == null)
                throw new ArgumentNullException(nameof(email));

            if (claim == null)
                throw new ArgumentNullException(nameof(claim));

            var user = await this.UserManager.FindByEmailAsync(email);
            if (user == null)
                return this.NotFound();

            var claims = await this.UserManager.GetClaimsAsync(user) ?? new List<SecurityClaim>();

            if (claims.Any(i => i.Type == claim.Type && i.Value == claim.Value))
                return this.Ok();

            var newClaim = new SecurityClaim(claim.Type, claim.Value);

            var result = await this.UserManager.AddClaimAsync(user, newClaim);

            return this.ConvertIdentityResultToResponse(result);
        }

        [HttpDelete("{email}")]
        [Consumes(ContentTypes.ApplicationJson)]
        [Produces(ContentTypes.ApplicationJson)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(
            [FromRoute]string email,
            [FromBody]LocalClaim claim)
        {
            if (email == null)
                throw new ArgumentNullException(nameof(email));

            if (claim == null)
                throw new ArgumentNullException(nameof(claim));

            var user = await this.UserManager.FindByEmailAsync(email);
            if (user == null)
                return this.NotFound();

            var claims = await this.UserManager.GetClaimsAsync(user) ?? new List<SecurityClaim>();

            var existingClaim = claims.FirstOrDefault(i => i.Type == claim.Type && i.Value == claim.Value);
            if (existingClaim == null)
                return this.NotFound();

            var result = await this.UserManager.RemoveClaimAsync(user, existingClaim);

            return this.ConvertIdentityResultToResponse(result);
        }
    }
}
