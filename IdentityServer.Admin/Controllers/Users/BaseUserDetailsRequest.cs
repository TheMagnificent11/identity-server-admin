using System.ComponentModel.DataAnnotations;
using IdentityServer.Common.Constants.Data;

namespace IdentityServer.Admin.Controllers.Users
{
    public abstract class BaseUserDetailsRequest : BaseUserRequest
    {
        [Required]
        [MaxLength(UserFieldMaxLengths.GivenName)]
        public string GivenName { get; set; }

        [Required]
        [MaxLength(UserFieldMaxLengths.Surname)]
        public string Surname { get; set; }
    }
}
