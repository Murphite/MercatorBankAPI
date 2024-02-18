using MercatorBankAPIProj.Models.Entities;

namespace MercatorBankAPIProj.Data.Repositories.Interface
{
    public interface ICardRepository
    {
        Task<Card> AddCardAsync(Card card);
        Task Delete(Card card);
        Task<Card> GetById(string id);
        Task<Card> UpdateCard(Card card);
        Task<List<Card>> GetCardsByMerchantUser(string userId);
        Task<Card> GetCardById(string cardId, string userId);
        Task<Card> GetCardByTransaction(string cardId, string transactionId);
    }
}
