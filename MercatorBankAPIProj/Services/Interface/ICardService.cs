using MercatorBankAPIProj.Models.Generics;
using MercatorBankAPIProj.Models.DTOs;

namespace MercatorBankAPIProj.Services.Interface
{
    public interface ICardService
    {
        Task<Result<AddCardResponseDTO>> AddCard(AddCardRequestDTO requestDTO, string userId);
        Task<Result<List<GetCardDTO>>> GetAllCards(string userId);
        Task<Result<List<ReqMagCardDTO>>> GetCardsByRequest(string userId);
        Task<Result<GetCardTransactionDTO>> GetCardByTransaction(string cardId, string userId);
        Task<Result<object>> DeleteCard(string cardId, string userId);
        Task<Result<UpdateCardDTO>> UpdateCard(string cardId, UpdateCardDTO requestDTO, string userId);
    }
}
