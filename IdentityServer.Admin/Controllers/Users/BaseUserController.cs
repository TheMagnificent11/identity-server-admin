using System;
using System.Linq;
using IdentityServer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Admin.Controllers.Users
{
    public abstract class BaseUserController : Controller
    {
        protected BaseUserController(UserManager<User> userManager)
        {
            this.UserManager = userManager;
        }

        protected UserManager<User> UserManager { get; }

        protected IActionResult ConvertIdentityResultToResponse(IdentityResult result)
        {
            if (result == null)
                throw new ArgumentNullException(nameof(result));

            if (result.Succeeded) return this.Ok();

            result.Errors
                .ToList()
                .ForEach(i => this.ModelState.AddModelError(i.Code, i.Description));

            return this.BadRequest(this.ModelState);
        }
    }
}
