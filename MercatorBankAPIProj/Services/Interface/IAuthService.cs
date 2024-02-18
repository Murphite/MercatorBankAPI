using MercatorBankAPIProj.Models.DTOs;
using MercatorBankAPIProj.Models.Entities;
using MercatorBankAPIProj.Models.Generics;

namespace MercatorBankAPIProj.Services.Interface
{
    public interface IAuthService
    {
        Task<Result<LoginResponseDTO>> Login(LoginRequestDTO loginRequestDTO);
        Task<Result<MerchantUserDTO>> Register(RegistrationRequestDTO requestDTO);
        Task<Result<bool>> AssignRole(string email, string roleName);
    }
}
