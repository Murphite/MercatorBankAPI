using MercatorBankAPIProj.Models.Enums;
using System;

namespace MercatorBankAPIProj.Models.DTOs
{
    public class ReqMagCardDTO
    {
        public string BatchNumber { get; set; }
        public int Quantity { get; set; }
        public CardCurrency Currency { get; set; }
        public CardScheme CardScheme { get; set; }
        public CardType CardType { get; set; }
        public CardStatus Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
    }
}
