using MercatorBankAPIProj.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MercatorBankAPIProj.Models.Entities
{
    public class CardRequestHist : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string BatchNumberId {  get; set; }
        public CardCurrency CardCurrency { get; set; }
        public CardScheme CardScheme { get; set; }
        public CardType CardType { get; set; }
        public CardStatus Status { get; set; }
        public int Quantity { get; set; }
        public List<Card> Cards { get; set; }


    }
}
