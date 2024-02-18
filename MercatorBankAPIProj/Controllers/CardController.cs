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
    public class CardController : Controller
    {
        private readonly ICardService _cardService;
        private readonly SignInManager<MerchantUser> _signInManager;
        private readonly ICardRepository _cardRepository;

        public CardController(ICardService cardService, SignInManager<MerchantUser> signInManager, ICardRepository cardRepository)
        {
            _cardService = cardService;
            _signInManager = signInManager;
            _cardRepository = cardRepository;

        }
        [HttpPost("add-card")]
        //[Authorize(Roles = "REGULAR")]
        public async Task<IActionResult> AddCard([FromBody] AddCardRequestDTO requestDTO)
        {
            var result = new Result<AddCardResponseDTO>();

            var user = await _signInManager.UserManager.GetUserAsync(User);

            result.RequestTime = DateTime.Now;

            var response = await _cardService.AddCard(requestDTO, user.Id);

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

        [HttpGet("all-cards")]
        //[Authorize(Roles = "REGULAR")]
        // [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetCards()
        {
            var result = new Result<List<GetCardDTO>>();

            var user = await _signInManager.UserManager.GetUserAsync(User);

            result.RequestTime = DateTime.Now;

            var response = await _cardService.GetAllCards(user.Id);

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


        [HttpDelete("cards/delete/{cardId}")]
        //[Authorize(Roles = "REGULAR")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(string cardId)
        {
            var result = new Result<object>();

            var user = await _signInManager.UserManager.GetUserAsync(User);

            result.RequestTime = DateTime.Now;

            var response = await _cardService.DeleteCard(cardId, user.Id);

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


        [HttpPut("update-contact/{cardId}")]
        //[Authorize(Roles = "REGULAR")]
        public async Task<IActionResult> UpdateContact(string cardId, UpdateCardDTO cardDTO)
        {
            var result = new Result<UpdateCardDTO>();

            var user = await _signInManager.UserManager.GetUserAsync(User);

            result.RequestTime = DateTime.Now;

            var response = await _cardService.UpdateCard(cardId, cardDTO, user.Id);

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

        //[HttpGet("request-card")]
        ////[Authorize(Roles = "REGULAR")]
        //// [Authorize(Roles = "admin")]
        //public async Task<IActionResult> RequestCard()
        //{
        //    var result = new Result<List<ReqMagCardDTO>>();

        //    var user = await _signInManager.UserManager.GetUserAsync(User);

        //    result.RequestTime = DateTime.Now;

        //    var response = await _cardService.GetCardsByRequest(user.Id);

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

        //[HttpGet("get-card/{cardId}")]
        ////[Authorize(Roles = "REGULAR")]
        ////[Authorize(Roles = "admin")]
        //public async Task<IActionResult> GetCard(string cardId)
        //{
        //    var result = new Result<GetCardTransactionDTO>();

        //    var user = await _signInManager.UserManager.GetUserAsync(User);

        //    result.RequestTime = DateTime.Now;

        //    var response = await _cardService.GetCardByTransaction(cardId, user.Id);

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


    }
}
