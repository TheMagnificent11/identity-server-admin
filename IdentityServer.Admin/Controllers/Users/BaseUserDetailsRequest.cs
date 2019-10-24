using System.ComponentModel.DataAnnotations;
using IdentityServer.Common.Constants.Data;

namespace IdentityServer.Admin.Controllers.Users
{
    public abstract class BaseUserDetailsRequest
    {
        [Required]
        [EmailAddress]
        [MaxLength(UserFieldMaxLengths.Email)]
        public string Email { get; set; }

        [Required]
        [MaxLength(UserFieldMaxLengths.GivenName)]
        public string GivenName { get; set; }

        [Required]
        [MaxLength(UserFieldMaxLengths.Surname)]
        public string Surname { get; set; }
    }
}
