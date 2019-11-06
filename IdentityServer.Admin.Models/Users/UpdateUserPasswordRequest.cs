using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Admin.Models.Users
{
    public class UpdateUserPasswordRequest
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}
