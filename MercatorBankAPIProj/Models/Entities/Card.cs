using MercatorBankAPIProj.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercatorBankAPIProj.Models.Entities
{
    public class Card : BaseEntity
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Name { get; set; }

        //CARD LIST
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Balance { get; set; }
        public string? CardNumber { get; set; }
        public string? AccountNumber { get; set; }
        public DateTime? Expiry { get; set; }

        //CARD REQUEST
        public int? Quantity { get; set; }
        public CardCurrency? CardCurrency { get; set; }
        public CardScheme? CardScheme { get; set; }
        public CardType? CardType { get; set; }
        public CardStatus? Status { get; set; }


        public string? MerchantUserId { get; set; }
        public string? TransactionId { get; set; }
       

        //Navigation Property
        public MerchantUser? MerchantUser { get; set; }
        public virtual List<Transaction>? Transaction { get; set; }
    }
}
