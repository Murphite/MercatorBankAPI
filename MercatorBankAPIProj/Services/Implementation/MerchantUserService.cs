using MercatorBankAPIProj.Data;
using MercatorBankAPIProj.Data.Repositories.Interface;
using MercatorBankAPIProj.Models.DTOs;
using MercatorBankAPIProj.Models.Entities;
using MercatorBankAPIProj.Models.Generics;
using MercatorBankAPIProj.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MercatorBankAPIProj.Services.Implementation
{
    public class MerchantUserService : IMerchantUserService
    {
        private readonly IMerchantUserRepository _merchantUserRepository;
        private readonly UserManager<MerchantUser> _userManager;
        private readonly MyDbContext _db;

        public MerchantUserService(IMerchantUserRepository merchantUserRepository, UserManager<MerchantUser> userManager, MyDbContext db)
        {
            _merchantUserRepository = merchantUserRepository;
            _userManager = userManager;
            _db = db;
        }

        public Task<Result<BalManageDTO>> GetUserById(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<List<GetUserDTO>>> GetUsersAsync()
        {
            var result = new Result<List<GetUserDTO>>();

            try
            {
                var users = await _merchantUserRepository.GetAllUsers();

                if (users.Count == 0)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "No users found";
                    return result;
                }

                var getUsers = new List<GetUserDTO>();

                foreach (var user in users)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    var getUser = new GetUserDTO
                    {
                        Id = user.Id,
                        AccountName = user.AccountName,
                        Email = user.Email

                        //CreatedTime = user.CreatedTime,
                        //CreatedDate = user.CreatedDate,
                        //RoleName = roles
                    };

                    getUsers.Add(getUser);
                }

                result.IsSuccess = true;
                result.Message = "Users retrieved successfully";
                result.Content = getUsers;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public async Task<Result<UpdateUserDTO>> UpdateUserAsync(string userId, UpdateUserDTO requestDTO)
        {
            var result = new Result<UpdateUserDTO>();
            try
            {
                var user = await _db.MerchantUsers.FirstOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "User not found";
                    return result;
                }


                user.Email = requestDTO.Email ?? user.Email;
                user.AccountName = requestDTO.Name ?? user.AccountName;
                user.UserName = requestDTO.UserName ?? user.UserName;


                var updateUser = await _merchantUserRepository.UpdateUser(user);

                var updateDTO = new UpdateUserDTO
                {
                    Name = updateUser.AccountName,
                    UserName = updateUser.UserName,
                    Email = updateUser.Email,
                };

                result.IsSuccess = true;
                result.Message = "User updated successfully";
                result.Content = updateDTO;


            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public async Task<Result<object>> DeleteUser(string userId)
        {
            var result = new Result<object>();
            try
            {
                var user = await _db.MerchantUsers.FirstOrDefaultAsync(u => u.Id == userId);
                if (user == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "User not found";
                    return result;
                }


                await _merchantUserRepository.Delete(user);
                //contact.DeletedAt = DateTime.Now;

                result.IsSuccess = true;
                result.Message = "User deleted successfully";
                //result.Content = new { DeletedAt = contact.DeletedAt };


            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        //public async Task<Result<BalManageDTO>> GetUserById(string userId)
        //{
        //    var result = new Result<BalManageDTO>();
        //    try
        //    {
        //        var user = await _merchantUserRepository.GetUserById(userId);

        //        if (user == null)
        //        {
        //            result.IsSuccess = false;
        //            result.ErrorMessage = "User not found";
        //            return result;
        //        }

        //        var getUser = new BalManageDTO
        //        {
        //            Id = user.Id,
        //            AccountName = user.AccountName,
        //            AccountBalance = MoreUserDetails.AccountBalance,
        //            Amount = user.Amount,
        //            Recipient = user.Recipient,
        //            AccountNumber = user.AccountNumber,
        //            Narration = user.Narration,
        //            CreatedAt = user.CreatedAt,
        //            TranType = user.Type,
        //            BankName = user.BankName

        //        };

        //        result.IsSuccess = true;
        //        result.Message = "User retrieved successfully";
        //        result.Content = getUser;

        //    }
        //    catch (Exception ex)
        //    {
        //        result.IsSuccess = false;
        //        result.ErrorMessage = ex.Message;
        //    }

        //    return result;
        //}

    }
}
