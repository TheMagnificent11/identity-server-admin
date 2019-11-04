using System;
using System.Linq;
using System.Threading.Tasks;
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
        public UsersController(UserManager<User> userManager)
            : base(userManager)
        {
        }

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

        // TODO: allow a user to update their own details
        [HttpPut(Name = "UpdateUser")]
        [Route("{email}")]
        [Consumes(ContentTypes.ApplicationJson)]
        [Produces(ContentTypes.ApplicationJson)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Put([FromRoute]string email, [FromBody]UpdateUserDetailsRequest request)
        {
            if (email == null)
                throw new ArgumentNullException(nameof(email));

            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var user = await this.UserManager.FindByNameAsync(email);
            if (user == null) return this.NotFound();

            user.GiveName = request.GivenName;
            user.Surname = request.Surname;

            await this.UserManager.UpdateAsync(user);

            return this.Ok();
        }
    }
}
