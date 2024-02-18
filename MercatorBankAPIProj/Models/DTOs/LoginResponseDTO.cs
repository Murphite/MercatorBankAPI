namespace MercatorBankAPIProj.Models.DTOs
{
    public class LoginResponseDTO
    {
        public MerchantUserDTO User { get; set; }
        public string Token { get; set; }
    }
}
