using System;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Data
{
    public class User : IdentityUser<Guid>
    {
        public string GiveName { get; set; }

        public string Surname { get; set; }
    }
}
