using System;
using System.Threading.Tasks;
using AutoMapper;
using IdentityServer.Admin.Authorization;
using IdentityServer.Admin.Models;
using IdentityServer.Common.Constants;
using IdentityServer.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Admin.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = Policies.ManageUsers)]
    public sealed class UsersController : BaseUserController
    {
        public UsersController(UserManager<User> userManager, IMapper mapper)
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

            var user = this.Mapper.Map<User>(request);

            var result = await this.UserManager.CreateAsync(user, request.Password);
            return this.ConvertIdentityResultToResponse(result);
        }

        // TODO: allow a user to access their own details
        [HttpGet("{email}", Name = "GetUser")]
        [Produces(ContentTypes.ApplicationJson)]
        [ProducesResponseType(200, Type = typeof(UserDetails))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetOne([FromRoute(Name = "email")]string email)
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
            [FromRoute(Name = "email")]string email,
            [FromBody]UpdateUserDetailsRequest request)
        {
            if (email == null)
                throw new ArgumentNullException(nameof(email));

            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var user = await this.UserManager.FindByEmailAsync(email);
            if (user == null)
                return this.NotFound();

            user.GivenName = request.GivenName;
            user.Surname = request.Surname;

            await this.UserManager.UpdateAsync(user);

            return this.Ok();
        }
    }
}
