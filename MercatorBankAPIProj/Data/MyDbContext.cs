using MercatorBankAPIProj.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MercatorBankAPIProj.Data
{
    public class MyDbContext : IdentityDbContext<MerchantUser>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }


        public DbSet<MerchantUser> MerchantUsers { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Transaction> Transactions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Card>()
                .HasOne(c => c.MerchantUser)
                .WithMany(u => u.Cards)
                .HasForeignKey(c => c.MerchantUserId)
                .IsRequired();

            modelBuilder.Entity<Transaction>()
                .HasOne(c => c.Card)
                .WithMany(u => u.Transaction)
                .HasForeignKey(c => c.CardId)
                .IsRequired();

            //modelBuilder.Entity<Card>()
            //    .HasOne(c => c.CardRequest)
            //    .WithMany(u => u.Cards)
            //    .HasForeignKey(c => c.CardRequestId);

            //modelBuilder.Entity<Card>()
            //    .HasOne(c => c.CardList)
            //    .WithMany(u => u.Cards)
            //    .HasForeignKey(c => c.CardListId);
        }
    }

}
