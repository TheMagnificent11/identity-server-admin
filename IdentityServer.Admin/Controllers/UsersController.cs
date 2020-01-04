using System;
using System.Threading.Tasks;
using AutoMapper;
using IdentityModel;
using IdentityServer.Admin.Authorization;
using IdentityServer.Admin.Models;
using IdentityServer.Common.Constants;
using IdentityServer.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SystemClaim = System.Security.Claims.Claim;

namespace IdentityServer.Admin.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = Policies.ManageUsers)]
    public sealed class UsersController : BaseUserController
    {
        public UsersController(UserManager<ApplicationUser> userManager, IMapper mapper)
            : base(userManager)
        {
            this.Mapper = mapper;
        }

        private IMapper Mapper { get; }

        [HttpPost(Name = "CreateUser")]
        [Consumes(ContentTypes.ApplicationJson)]
        [Produces(ContentTypes.ApplicationJson)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> Post([FromBody]RegistrationRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var user = this.Mapper.Map<ApplicationUser>(request);

            var result = await this.UserManager.CreateAsync(user, request.Password);
            return this.ConvertIdentityResultToResponse(result);
        }

        // TODO: allow a user to access their own details
        [HttpGet("{email}", Name = "GetUser")]
        [Produces(ContentTypes.ApplicationJson)]
        [ProducesResponseType(200, Type = typeof(UserDetails))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetOne([FromRoute]string email)
        {
            if (email == null)
                throw new ArgumentNullException(nameof(email));

            var user = await this.UserManager.FindByEmailAsync(email);
            if (user == null)
                return this.NotFound();

            return this.Ok(this.Mapper.Map<UserDetails>(user));
        }

        // TODO: allow a user to update their own details
        [HttpPut("{email}", Name = "UpdateUser")]
        [Consumes(ContentTypes.ApplicationJson)]
        [Produces(ContentTypes.ApplicationJson)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Put(
            [FromRoute]string email,
            [FromBody]UpdateUserDetailsRequest request)
        {
            if (email == null)
                throw new ArgumentNullException(nameof(email));

            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var user = await this.UserManager.FindByEmailAsync(email);
            if (user == null)
                return this.NotFound();

            var result = await this.UserManager.UpdateAsync(user);

            if (!result.Succeeded)
                return this.ConvertIdentityResultToResponse(result);

            var claims = new SystemClaim[]
            {
                new SystemClaim(JwtClaimTypes.Name, $"{request.GivenName} {request.Surname}"),
                new SystemClaim(JwtClaimTypes.GivenName, request.GivenName),
                new SystemClaim(JwtClaimTypes.FamilyName, request.Surname),
                new SystemClaim(JwtClaimTypes.Email, request.Email)
            };

            result = await this.UserManager.AddClaimsAsync(user, claims);

            return this.ConvertIdentityResultToResponse(result);
        }
    }
}
