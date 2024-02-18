using MercatorBankAPIProj.Models.Enums;

namespace MercatorBankAPIProj.Models.DTOs
{
    public class UpdateCardDTO
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public CardCurrency CardCurrency { get; set; }
        public CardScheme CardScheme { get; set; }
        public CardType CardType { get; set; }
    }
}
