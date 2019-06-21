using System.ComponentModel.DataAnnotations;
using IdentityServer.Common.Constants.Data;

namespace IdentityServer.Admin.Controllers.Users
{
    public abstract class BaseUserRequest
    {
        [Required]
        [EmailAddress]
        [MaxLength(UserFieldMaxLengths.Email)]
        public string Email { get; set; }
    }
}
