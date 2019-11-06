using System;
using System.Threading.Tasks;
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
    [Authorize(Policy = Policies.ManageUsers)] // TODO: allows users to set their own password
    public sealed class UserPasswordsController : BaseUserController
    {
        public UserPasswordsController(UserManager<User> userManager)
            : base(userManager)
        {
        }

        [HttpPut(Name = "UpdatePasswordForUser")]
        [Route("{email}/password")]
        [Consumes(ContentTypes.ApplicationJson)]
        [Produces(ContentTypes.ApplicationJson)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutPassword([FromRoute]string email, [FromBody]UpdateUserPasswordRequest request)
        {
            if (email == null)
                throw new ArgumentNullException(nameof(email));

            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var user = await this.UserManager.FindByEmailAsync(email);
            if (user == null)
                return this.NotFound();

            var result = await this.UserManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            return this.ConvertIdentityResultToResponse(result);
        }
    }
}
