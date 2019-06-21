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
    public sealed class UsersController : Controller
    {
        public UsersController(UserManager<User> userManager)
        {
            this.UserManager = userManager;
        }

        private UserManager<User> UserManager { get; }

        [HttpPost]
        [Consumes(ContentTypes.ApplicationJson)]
        [Produces(ContentTypes.ApplicationJson)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> Post([FromBody]RegistrationRequest request)
        {
            var user = new User
            {
                GiveName = request.GivenName,
                Surname = request.Surname,
                Email = request.Email,
                UserName = request.Email
            };

            var result = await this.UserManager.CreateAsync(user, request.Password);
            if (result.Succeeded) return this.Ok();

            result.Errors
                .ToList()
                .ForEach(i => this.ModelState.AddModelError(i.Code, i.Description));

            return this.BadRequest(this.ModelState);
        }

        [HttpPut]
        [Consumes(ContentTypes.ApplicationJson)]
        [Produces(ContentTypes.ApplicationJson)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Put([FromBody]UpdateUserDetailsRequest request)
        {
            var user = await this.UserManager.FindByNameAsync(request.Email);
            if (user == null) return this.NotFound();

            user.GiveName = request.GivenName;
            user.Surname = request.Surname;

            await this.UserManager.UpdateAsync(user);

            return this.Ok();
        }
    }
}
