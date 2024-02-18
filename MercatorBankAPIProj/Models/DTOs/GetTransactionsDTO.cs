using MercatorBankAPIProj.Models.Enums;

namespace MercatorBankAPIProj.Models.DTOs
{
    public class GetTransactionsDTO
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string CardNumber { get; set; }
        public string AccountNumber { get; set; }
        public string ReferenceNumber { get; set; }
        public decimal Amount { get; set; }
        public TranType Type { get; set; }
        public DateTime? CreatedAt { get; set; }


    }
}
