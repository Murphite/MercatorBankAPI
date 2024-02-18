using MercatorBankAPIProj.Data.Repositories.Interface;
using MercatorBankAPIProj.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MercatorBankAPIProj.Data.Repositories.Implementations
{
    public class CardRepository : ICardRepository
    {
        private readonly MyDbContext _db;

        public CardRepository(MyDbContext db)
        {
            _db = db;
        }

        public async Task<Card> AddCardAsync(Card card)
        {
            var newCard = await _db.Cards.AddAsync(card);
            newCard.Entity.CreatedAt = DateTime.UtcNow;
            newCard.Entity.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();

            return newCard.Entity;
        }

        public async Task Delete(Card card)
        {
            _db.Cards.Remove(card);
            await _db.SaveChangesAsync();
        }

        public async Task<Card> GetById(string id)
        {
            var contact = await _db.Cards.Include(x => x.MerchantUser).Where(x => x.Id == id).FirstOrDefaultAsync();

            if (contact == null)
            {
                throw new Exception("User does not exist");
            }
            return contact;
        }

        public async Task<Card> GetCardById(string cardId, string userId)
        {
            var card = await _db.Cards.FirstOrDefaultAsync(c => c.MerchantUserId == userId && c.Id == cardId);

            return card;
        }
        public async Task<Card> GetCardByTransaction(string cardId, string transactionId)
        {
            var card = await _db.Cards.FirstOrDefaultAsync(c => c.TransactionId == transactionId && c.Id == cardId);

            return card;
        }

        public async Task<List<Card>> GetCardsByMerchantUser(string userId)
        {
            var cards = await _db.Cards.Where(c => c.MerchantUserId == userId).ToListAsync();

            return cards;
        }

        public async Task<Card> UpdateCard(Card card)
        {
            var newCard = _db.Cards.Update(card);
            newCard.Entity.UpdatedAt = DateTime.Now;
            await _db.SaveChangesAsync();


            return newCard.Entity;
        }
    }
}
