using Transactions.Persistence.Models;

namespace Transactions.Persistence.Repositories
{
    public interface IAccountRepository
    {
        public Account Create(Account account);
        public Account GetById(int accountId);
        public bool AccountExist(int accountId);
        public double Transfer(Account account);
        public Account Deposit(Account account, double value);
        public Account Withdraw(Account account, double value);
        public bool VerifyBalance(Account account, double value);
        public double GetBalance(Account account);
    }
}
