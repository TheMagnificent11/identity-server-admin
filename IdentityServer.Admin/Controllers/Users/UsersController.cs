using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Common.Constants;
using IdentityServer.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers.Users
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public sealed class UsersController : Controller
    {
        public UsersController(UserManager<User> userManager)
        {
            UserManager = userManager;
        }

        private UserManager<User> UserManager { get; }

        [HttpPost]
        [Consumes(ContentTypes.ApplicationJson)]
        [Produces(ContentTypes.ApplicationJson)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> Post([FromBody]RegistrationRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = new User
            {
                GiveName = request.GivenName,
                Surname = request.Surname,
                Email = request.Email,
                UserName = request.Email
            };

            var result = await UserManager.CreateAsync(user, request.Password);
            if (result.Succeeded) return Ok();

            result.Errors
                .ToList()
                .ForEach(i => ModelState.AddModelError(i.Code, i.Description));

            return BadRequest(ModelState);
        }
    }
}
