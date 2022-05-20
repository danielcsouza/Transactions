using Transactions.Domains;
using Transactions.Persistence.ViewModels;
using Transactions.Services.ViewModels;

namespace Transactions.Persistence.Repositories
{
    public interface IAccountRepository
    {
        public Account Create(Account account);
        public Account GetById(string? accountId);
        public bool AccountExist(string? accountId);
        public Account Deposit(Account account, double value);
        public Account Withdraw(Account account, double value);
        public bool VerifyBalance(Account account, double value);
        public double GetBalance(Account account);
        public TransferDataViewModel Transfer(Account origin, Account destination, double value);
        public void Reset();
        
    }
}
