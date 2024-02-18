using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercatorBankAPIProj.Models.DTOs
{
    public class RegistrationRequestDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }

        //[ForeignKey("MoreUserDetails")]
        //public int? ReferenceId { get; set; }
    }
}
