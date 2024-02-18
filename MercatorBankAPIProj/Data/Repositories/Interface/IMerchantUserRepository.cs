using MercatorBankAPIProj.Models.Entities;

namespace MercatorBankAPIProj.Data.Repositories.Interface
{
    public interface IMerchantUserRepository
    {
        Task<List<MerchantUser>> GetAllUsers();
        Task<MerchantUser> GetById(string Id);
        Task<MerchantUser> UpdateUser(MerchantUser user);
        Task Delete(MerchantUser user);
        Task<MerchantUser> GetUserById(string userId);
    }
}
