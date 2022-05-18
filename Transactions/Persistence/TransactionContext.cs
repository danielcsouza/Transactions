using Microsoft.EntityFrameworkCore;
using Transactions.Persistence.Models;

namespace Transactions.Persistence
{
    public class TransactionContext : DbContext
    {
        public TransactionContext(DbContextOptions<TransactionContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasKey(d => d.Id);
        }
    }
   
}
