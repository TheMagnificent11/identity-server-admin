using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Admin.Models
{
    public class Claim
    {
        [Required]
        public string Type { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
