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

        public Account Create(Account account)
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();

            return account;
        }

        public Account Deposit(Account account, double value)
        {
            account.setBalance(value, Enums.OperationsEnum.Deposit);

            _context.Accounts.Add(account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            

            return account;
        }

        public Account? GetById(int accountId)
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

        public Account Withdraw(Account account, double value)
        {
             account.setBalance(value, Enums.OperationsEnum.withdraw);
                        
            _context.Accounts.Add(account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return account;
        }

        public bool AccountExist(int accountId)
        {
            return _context.Accounts.Any(e => e.Id == accountId);
        }

        public bool VerifyBalance(Account account, double value)
        {
          return account.VerifyBalanceWithDraw(value);
        }
    }
}
