using MercatorBankAPIProj.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercatorBankAPIProj.Models.Entities
{
    public class Transaction : BaseEntity
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Recipient { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Amount { get; set; }
        public TranType? Type { get; set; }
        public string? Location { get; set; }
        public TranStatus? Status { get; set; }
        public RequestStatus? RequestStatus { get; set; }
        public string? RefNumber { get; set; }
        public string? CardId { get; set; }
        public string? TerminalId { get; set; }
        public string? MerchantUserId { get; set; }



        //Navigation Property
        public MerchantUser? MerchantUser { get; set; }
        public Card? Card { get; set; }
        

    }

}








