using MercatorBankAPIProj.Models.DTOs;
using MercatorBankAPIProj.Models.Generics;

namespace MercatorBankAPIProj.Services.Interface
{
    public interface ITransactionService
    {
        Task<Result<AddTransactionResponseDTO>> AddTransaction(AddTransactionRequestDTO requestDTO, string cardId);
        Task<Result<List<GetTransactionsDTO>>> GetAllTransactions(string userId);

    }
}
