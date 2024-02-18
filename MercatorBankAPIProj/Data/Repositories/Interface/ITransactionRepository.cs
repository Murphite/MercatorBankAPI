using MercatorBankAPIProj.Models.Entities;

namespace MercatorBankAPIProj.Data.Repositories.Interface
{
    public interface ITransactionRepository
    {
        Task<Transaction> AddTransactionAsync(Transaction transaction);
        //Task Delete(Transaction transaction);
        Task<Transaction> GetById(string id);
        Task<List<Transaction>> GetTransactionsByCardId(string cardId);
    }
}
