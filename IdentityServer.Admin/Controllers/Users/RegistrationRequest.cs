using System.ComponentModel.DataAnnotations;
using IdentityServer.Common.Constants.Data;

namespace IdentityServer.Controllers.Users
{
    public class RegistrationRequest
    {
        [Required]
        [MaxLength(UserFieldMaxLengths.GivenName)]
        public string GivenName { get; set; }

        [Required]
        [MaxLength(UserFieldMaxLengths.Surname)]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(UserFieldMaxLengths.Email)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
