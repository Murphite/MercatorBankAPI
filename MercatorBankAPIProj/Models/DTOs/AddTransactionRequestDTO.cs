using MercatorBankAPIProj.Models.Enums;

namespace MercatorBankAPIProj.Models.DTOs
{
    public class AddTransactionRequestDTO
    {
        //public string TerminalId { get; set; }
        public string CardNumber { get; set; }
        public decimal Amount { get; set; }
        //public string RefNumber { get; set; }
        public TranType Type {get; set; }
        //public string? MerchantInfo { get; set; }
        //public TranStatus Status { get; set; }
        //public DateTime CreatedAt { get; set; }
    }
}
