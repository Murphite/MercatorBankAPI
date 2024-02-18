using MercatorBankAPIProj.Models.Enums;

namespace MercatorBankAPIProj.Models.DTOs
{
    public class AddCardResponseDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public CardCurrency CardCurrency { get; set; }
        public CardScheme CardScheme { get; set; }
        public CardType CardType { get; set; }
        public string? AddedBy { get; set; }
        public DateTime Expiry { get; set; }


    }
}
