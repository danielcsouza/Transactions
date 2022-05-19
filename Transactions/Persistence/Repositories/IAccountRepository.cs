using Transactions.Persistence.Models;
using Transactions.Persistence.ViewModels;

namespace Transactions.Persistence.Repositories
{
    public interface IAccountRepository
    {
        public Account Create(Account account);
        public Account GetById(int accountId);
        public bool AccountExist(int accountId);
        public Account Deposit(Account account, double value);
        public Account Withdraw(Account account, double value);
        public bool VerifyBalance(Account account, double value);
        public double GetBalance(Account account);
        public TransferDataViewModel Transfer(Account origin, Account destination, double value);
    }
}
