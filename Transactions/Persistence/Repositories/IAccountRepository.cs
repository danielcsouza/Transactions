using Transactions.Persistence.Models;

namespace Transactions.Persistence.Repositories
{
    public interface IAccountRepository
    {
        public Account Get(int accountId);
        public double Transfer(Account account);
        public double Deposit(Account account);
        public double Withdraw(Account account);
        public double GetBalance(Account account);
    }
}
