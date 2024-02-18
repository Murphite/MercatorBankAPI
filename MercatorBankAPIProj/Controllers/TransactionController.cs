using MercatorBankAPIProj.Data.Repositories.Interface;
using MercatorBankAPIProj.Models.DTOs;
using MercatorBankAPIProj.Models.Entities;
using MercatorBankAPIProj.Models.Generics;
using MercatorBankAPIProj.Services.Implementation;
using MercatorBankAPIProj.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MercatorBankAPIProj.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly ICardRepository _cardRepository;
        private readonly SignInManager<MerchantUser> _signInManager;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionController(ITransactionService transactionService, ICardRepository cardRepository, SignInManager<MerchantUser> signInManager, ITransactionRepository transactionRepository)
        {

            _transactionService = transactionService;
            _cardRepository = cardRepository;
            _signInManager = signInManager;
            _transactionRepository = transactionRepository;

        }

        [HttpPost("add-transaction")]
        //[Authorize(Roles = "REGULAR")]
        public async Task<IActionResult> AddTransaction(string Id, [FromBody] AddTransactionRequestDTO requestDTO)
        {
            var result = new Result<AddTransactionResponseDTO>();

            //var user = await _signInManager.UserManager.GetUserAsync(User);
            var card = await _cardRepository.GetById(Id);

            result.RequestTime = DateTime.Now;

            var response = await _transactionService.AddTransaction(requestDTO, card.Id);

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


        [HttpGet("all-transactions")]
        //[Authorize(Roles = "REGULAR")]
        // [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetTransactions(string Id)
        {
            var result = new Result<List<GetTransactionsDTO>>();

            //var user = await _signInManager.UserManager.GetUserAsync(User);
            var card = await _cardRepository.GetById(Id); 

            result.RequestTime = DateTime.Now;

            var response = await _transactionService.GetAllTransactions(card.Id);

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
