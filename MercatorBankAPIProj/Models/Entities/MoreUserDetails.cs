using MercatorBankAPIProj.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercatorBankAPIProj.Models.Entities
{
    public class MoreUserDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReferenceId { get; set; }
        public string AccountBalance { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public string Recipient { get; set; }
        public string AccountNumber { get; set; }
        public string Narration { get; set; }
        public DateTime CreatedAt { get; set; }
        public TranType Type { get; set; }
        public string BankName { get; set; }
    }
}
