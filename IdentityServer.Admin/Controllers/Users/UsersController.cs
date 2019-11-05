using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IdentityServer.Admin.Authorization;
using IdentityServer.Common.Constants;
using IdentityServer.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Admin.Controllers.Users
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

            var user = new User
            {
                GiveName = request.GivenName,
                Surname = request.Surname,
                Email = request.Email,
                UserName = request.Email
            };

            var result = await this.UserManager.CreateAsync(user, request.Password);
            return this.ConvertIdentityResultToResponse(result);
        }

        // TODO: allow a user to access their own details
        [HttpGet("{user-id}", Name = "GetUser")]
        [Produces(ContentTypes.ApplicationJson)]
        [ProducesResponseType(200, Type = typeof(UserDetails))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetOne([FromRoute(Name = "user-id")]Guid userId)
        {
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));

            var user = await this.UserManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return this.NotFound();

            return this.Ok(this.Mapper.Map<UserDetails>(user));
        }

        [HttpGet(Name = "GetUsers")]
        [Produces(ContentTypes.ApplicationJson)]
        [ProducesResponseType(200, Type = typeof(IList<UserDetails>))]
        [ProducesResponseType(400, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> GetAll(
            [FromQuery(Name = "role")]string role = null,
            [FromQuery(Name = "email")]string email = null)
        {
            if (string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(role))
            {
                this.ModelState.AddModelError(string.Empty, "'email' or 'role' query parameter must be provided");
                return this.BadRequest(this.ModelState);
            }

            if (!string.IsNullOrWhiteSpace(role))
            {
                var roleUsers = await this.UserManager.GetUsersInRoleAsync(role.Trim()) ?? new List<User>();

                if (!string.IsNullOrWhiteSpace(email))
                {
                    roleUsers = roleUsers
                        .Where(i => i.Email.Equals(email.Trim(), StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }

                return this.Ok(this.Mapper.Map<List<UserDetails>>(roleUsers));
            }
            else
            {
                var user = await this.UserManager.FindByEmailAsync(email) ?? new User();
                var userDetails = this.Mapper.Map<UserDetails>(user);
                return this.Ok(new List<UserDetails> { userDetails });
            }
        }

        // TODO: allow a user to update their own details
        [HttpPut("{user-id}", Name = "UpdateUser")]
        [Consumes(ContentTypes.ApplicationJson)]
        [Produces(ContentTypes.ApplicationJson)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Put(
            [FromRoute(Name = "user-id")]Guid userId,
            [FromBody]UpdateUserDetailsRequest request)
        {
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));

            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var user = await this.UserManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return this.NotFound();

            user.GiveName = request.GivenName;
            user.Surname = request.Surname;

            await this.UserManager.UpdateAsync(user);

            return this.Ok();
        }
    }
}
