using MercatorBankAPIProj.Models.DTOs;
using MercatorBankAPIProj.Models.Generics;

namespace MercatorBankAPIProj.Services.Interface
{
    public interface IMerchantUserService
    {
        Task<Result<List<GetUserDTO>>> GetUsersAsync();

        Task<Result<UpdateUserDTO>> UpdateUserAsync(string cardId, UpdateUserDTO requestDTO);
        Task<Result<object>> DeleteUser(string userId);
        Task<Result<BalManageDTO>> GetUserById(string userId);
    }
}
