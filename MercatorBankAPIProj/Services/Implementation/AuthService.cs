using MercatorBankAPIProj.Data;
using MercatorBankAPIProj.Models.DTOs;
using MercatorBankAPIProj.Models.Entities;
using MercatorBankAPIProj.Models.Generics;
using MercatorBankAPIProj.Services.Interface;
using Microsoft.AspNetCore.Identity;

namespace MercatorBankAPIProj.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly MyDbContext _dbContext;
        private readonly UserManager<MerchantUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtService _jwtService;

        public AuthService(MyDbContext dbContext, UserManager<MerchantUser> userManager, RoleManager<IdentityRole> roleManager, IJwtService jwtService)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
        }
        public async Task<Result<bool>> AssignRole(string email, string roleName)
        {
            var result = new Result<bool>();
            try
            {
                var user = _dbContext.MerchantUsers.FirstOrDefault(u => u.Email.ToLower() == email);

                if (user == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "User not found";
                    return result;
                }

                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }

                await _userManager.AddToRoleAsync(user, roleName);

                result.IsSuccess = true;
                result.Message = "Role assigned successfully";
                result.Content = true;


            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public async Task<Result<LoginResponseDTO>> Login(LoginRequestDTO loginRequestDTO)
        {
            var result = new Result<LoginResponseDTO>();
            try
            {
                var user = _dbContext.MerchantUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDTO.Email.ToLower());

                if (user == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "User not found";
                    return result;
                }

                var isValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

                if (!isValid)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Password not valid";
                    return result;
                }

                var roles = await _userManager.GetRolesAsync(user);
                var token = _jwtService.GenerateToken(user, roles);

                var appUserDTO = new MerchantUserDTO
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.AccountName,
                    RoleName = roles
                };

                var loginResponseDTO = new LoginResponseDTO()
                {
                    User = appUserDTO,
                    Token = token
                };

                result.IsSuccess = true;
                result.Message = "User logged in successfully";
                result.Content = loginResponseDTO;



            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public async Task<Result<MerchantUserDTO>> Register(RegistrationRequestDTO requestDTO)
        {
            var result = new Result<MerchantUserDTO>();
            try
            {
                MerchantUser appUser = new MerchantUser
                {
                    Email = requestDTO.Email,
                    AccountName = requestDTO.Name,
                    UserName = requestDTO.Email

                };

                var newUser = await _userManager.CreateAsync(appUser, requestDTO.Password);

                if (newUser == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "User not added";
                    return result;
                }

                var userToReturn = _dbContext.MerchantUsers.First(u => u.UserName == requestDTO.Email);

                await AssignRole(userToReturn.Email, "REGULAR");

                var roles = await _userManager.GetRolesAsync(userToReturn);

                MerchantUserDTO appUserDTO = new MerchantUserDTO
                {
                    Id = userToReturn.Id,
                    Email = userToReturn.Email,
                    Name = userToReturn.AccountName,
                    RoleName = roles
                };

                result.IsSuccess = true;
                result.Message = "User registered successfully";
                result.Content = appUserDTO;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
