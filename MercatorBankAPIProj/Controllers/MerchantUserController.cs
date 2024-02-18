using MercatorBankAPIProj.Data.Repositories.Implementations;
using MercatorBankAPIProj.Data.Repositories.Interface;
using MercatorBankAPIProj.Models.DTOs;
using MercatorBankAPIProj.Models.Entities;
using MercatorBankAPIProj.Models.Generics;
using MercatorBankAPIProj.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MercatorBankAPIProj.Controllers
{
    public class MerchantUserController : Controller
    {
        private readonly IMerchantUserService _merchantUserService;
        private readonly SignInManager<MerchantUser> _signInManager;
        private readonly IMerchantUserRepository _merchantUserRepository;

        public MerchantUserController(IMerchantUserService userService, SignInManager<MerchantUser> signInManager, IMerchantUserRepository merchantUserRepository)
        {
            _merchantUserService = userService;
            _signInManager = signInManager;
            _merchantUserRepository = merchantUserRepository;
        }

        [HttpGet("all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = new Result<List<GetUserDTO>>();


            result.RequestTime = DateTime.Now;

            var response = await _merchantUserService.GetUsersAsync();

            if (response.IsSuccess)
            {
                result.ResponseTime = DateTime.Now;
                result = response;
                return Ok(result);
            }

            result = response;
            result.ResponseTime = DateTime.Now;
            return BadRequest(result);
        }


        //[HttpGet("get-user/{userId}")]
        //[Authorize(Roles = "REGULAR")]
        //[Authorize(Roles = "admin")]
        //public async Task<IActionResult> GetUser(string userId)
        //{
        //    var result = new Result<BalManageDTO>();

        //    //var user = await _signInManager.UserManager.GetUserAsync(User);

        //    result.RequestTime = DateTime.Now;

        //    var response = await _merchantUserService.GetUserById(userId);

        //    if (response.IsSuccess)
        //    {
        //        result.ResponseTime = DateTime.Now;
        //        result = response;
        //        return Ok(result);
        //    }

        //    result = response;
        //    result.ResponseTime = DateTime.Now;
        //    return BadRequest(result);

        //}


        [HttpPut("update-user/{userId}")]
        public async Task<IActionResult> UpdateUserAsync(string userId, UpdateUserDTO userDTO)
        {
            var result = new Result<UpdateUserDTO>();

            //var user = await _signInManager.UserManager.GetUserAsync(User);

            result.RequestTime = DateTime.Now;

            var response = await _merchantUserService.UpdateUserAsync(userId, userDTO);

            if (response.IsSuccess)
            {
                result.ResponseTime = DateTime.Now;
                result = response;
                return Ok(result);
            }

            result = response;
            result.ResponseTime = DateTime.Now;
            return BadRequest(result);

        }

        [HttpDelete("users/delete/{userId}")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(string userId)
        {
            var result = new Result<object>();

            //var user = await _signInManager.UserManager.GetUserAsync(User);

            result.RequestTime = DateTime.Now;

            var response = await _merchantUserService.DeleteUser(userId);

            if (response.IsSuccess)
            {
                result.ResponseTime = DateTime.Now;
                result = response;
                return Ok(result);
            }

            result = response;
            result.ResponseTime = DateTime.Now;
            return BadRequest(result);
        }

    }
}
