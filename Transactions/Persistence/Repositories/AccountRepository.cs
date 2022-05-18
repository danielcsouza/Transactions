using Transactions.Persistence.Models;

namespace Transactions.Persistence.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly TransactionContext  _context;

        public AccountRepository(TransactionContext context)
        {
            _context = context;
        }

        public double Deposit(Account account)
        {
            account.setBalance(0, Enums.OperationsEnum.Deposit);
            return account.Balance;
        }

        public Account Get(int accountId)
        {
            var account =  _context.Accounts.SingleOrDefault(e => e.Id == accountId); 
            if (account == null) return null;

            return account;
                    
        }

        public double GetBalance(Account account)
        {
            return account.Balance;
        }

        public double Transfer(Account account)
        {
            throw new NotImplementedException();
        }

        public double Withdraw(Account account)
        {
            account.setBalance(0, Enums.OperationsEnum.withdraw);
            return account.Balance;
        }
    }
}
