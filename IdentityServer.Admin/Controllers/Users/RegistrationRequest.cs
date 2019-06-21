using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Admin.Controllers.Users
{
    public class RegistrationRequest : BaseUserDetailsRequest
    {
        [Required]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
