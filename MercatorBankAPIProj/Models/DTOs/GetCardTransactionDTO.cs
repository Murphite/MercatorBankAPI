using MercatorBankAPIProj.Models.Enums;

namespace MercatorBankAPIProj.Models.DTOs
{
    public class GetCardTransactionDTO
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string CardNumber { get; set; }
        public string AccountNumber { get; set; }
        public DateTime Expiry { get; set; }
        public string Recipient { get; set; }
        public decimal Amount { get; set; }
        public CardStatus Status { get; set; }
        public RequestStatus RequestStatus { get; set; }
        public TranType Type { get; set; }
        public string Location { get; set; }
        public DateTime? CreatedAt { get; set; }


    }
}
