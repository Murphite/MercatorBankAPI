using MercatorBankAPIProj.Data.Repositories.Interface;
using MercatorBankAPIProj.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MercatorBankAPIProj.Data.Repositories.Implementations
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly MyDbContext _db;

        public TransactionRepository(MyDbContext db)
        {
            _db = db;
        }

        public async Task<Transaction> AddTransactionAsync(Transaction transaction)
        {
            var newTransaction = await _db.Transactions.AddAsync(transaction);
            newTransaction.Entity.CreatedAt = DateTime.UtcNow;
            newTransaction.Entity.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();

            return newTransaction.Entity;
        }

        //public async Task Delete(Transaction transaction)
        //{
        //    _db.Transactions.Remove(transaction);
        //    await _db.SaveChangesAsync();
        //}

        public async Task<Transaction> GetById(string id)
        {
            var contact = await _db.Transactions.Include(x => x.Card).Where(x => x.Id == id).FirstOrDefaultAsync();

            if (contact == null)
            {
                throw new Exception("Card does not exist");
            }
            return contact;
        }

        public async Task<List<Transaction>> GetTransactionsByCardId(string cardId)
        {
            var cards = await _db.Transactions.Where(c => c.CardId == cardId).ToListAsync();

            return cards;
        }

  
    }
}
