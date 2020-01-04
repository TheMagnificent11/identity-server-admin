﻿using System;
using System.Linq;
using IdentityServer.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Admin.Controllers
{
    public abstract class BaseUserController : Controller
    {
        protected BaseUserController(UserManager<ApplicationUser> userManager)
        {
            this.UserManager = userManager;
        }

        protected UserManager<ApplicationUser> UserManager { get; }

        protected IActionResult ConvertIdentityResultToResponse(IdentityResult result)
        {
            if (result == null)
                throw new ArgumentNullException(nameof(result));

            if (result.Succeeded)
                return this.Ok();

            result.Errors
                .ToList()
                .ForEach(i => this.ModelState.AddModelError(i.Code, i.Description));

            return this.BadRequest(this.ModelState);
        }
    }
}
