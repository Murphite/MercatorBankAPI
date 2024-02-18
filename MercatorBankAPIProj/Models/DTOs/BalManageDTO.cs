using MercatorBankAPIProj.Models.Enums;

namespace MercatorBankAPIProj.Models.DTOs
{
    public class BalManageDTO
    {
        public string Id { get; set; }
        public string AccountBalance { get; set; }
        public string AccountName { get; set; }
        public decimal Amount { get; set; }
        public string Recipient { get; set; }
        public string AccountNumber { get; set; }
        public string Narration { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime CreatedDate => CreatedAt.Date;
        public TimeSpan CreatedTime => CreatedAt.TimeOfDay;
        public TranType TranType { get; set; }
        public string BankName { get; set; }

    }
}
