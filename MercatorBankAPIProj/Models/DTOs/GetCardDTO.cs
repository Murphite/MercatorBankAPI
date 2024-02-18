namespace MercatorBankAPIProj.Models.DTOs
{
    public class GetCardDTO
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string CardNumber { get; set; }
        public string AccountNumber { get; set; }
        public DateTime Expiry { get; set; }
    }
}
