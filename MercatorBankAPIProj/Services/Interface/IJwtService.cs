using MercatorBankAPIProj.Models.Entities;

namespace MercatorBankAPIProj.Services.Interface
{
    public interface IJwtService
    {
        string GenerateToken(MerchantUser merchantUser, IEnumerable<string> roles);
    }
}
