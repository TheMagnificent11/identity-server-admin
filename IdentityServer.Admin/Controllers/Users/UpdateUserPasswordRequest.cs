using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Admin.Controllers.Users
{
    public class UpdateUserPasswordRequest
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}
