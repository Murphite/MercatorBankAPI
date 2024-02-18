using MercatorBankAPIProj.Data.Repositories.Interface;
using MercatorBankAPIProj.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MercatorBankAPIProj.Data.Repositories.Implementations
{
    public class MerchantUserRepository : IMerchantUserRepository
    {
        private readonly MyDbContext _db;

        public MerchantUserRepository(MyDbContext db)
        {
            _db = db;
        }
        public async Task<List<MerchantUser>> GetAllUsers()
        {
            var users = await _db.MerchantUsers.ToListAsync();
            return users;
        }

        public async Task<MerchantUser> GetUserById(string userId)
        {
            var user = await _db.MerchantUsers.FirstOrDefaultAsync(c => c.Id == userId);

            return user;
        }

        public async Task<MerchantUser> UpdateUser(MerchantUser user)
        {
            var newUser = _db.MerchantUsers.Update(user);
            //newUser.Entity.UpdatedAt = DateTime.Now;
            await _db.SaveChangesAsync();

            return newUser.Entity;
        }

        public async Task<MerchantUser> GetById(string Id)
        {
            var user = await _db.MerchantUsers.Include(x => x.Cards).Where(x => x.Id == Id).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new Exception("User does not exist");
            }
            return user;
        }                

        public async Task Delete(MerchantUser user)
        {
            _db.MerchantUsers.Remove(user);
            await _db.SaveChangesAsync();
        }

    }
}
