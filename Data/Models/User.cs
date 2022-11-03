using System.ComponentModel.DataAnnotations;

namespace OpenBankClient.Data.Models
{
    public class User
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Username { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }


    }
}
