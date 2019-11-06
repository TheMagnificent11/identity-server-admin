using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Admin.Models
{
    public class RegistrationRequest : BaseUserDetails
    {
        [Required]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
