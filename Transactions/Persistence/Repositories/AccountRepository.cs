using Transactions.Domains;
using Transactions.Persistence.ViewModels;
using Transactions.Services.ViewModels;

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

        public Account GetById(string? accountId)
        {
            var account =  _context.Accounts.SingleOrDefault(e => e.Id == accountId); 
            if (account == null) return new Account();

            return account;
                    
        }

        public double GetBalance(Account account)
        {
            return account.Balance;
        }

        public Account Withdraw(Account account, double value)
        {
             account.setBalance(value, Enums.OperationsEnum.withdraw);
                        
            _context.Accounts.Add(account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return account;
        }

        public bool AccountExist(string? accountId)
        {
            if (accountId == null) return false;
            return _context.Accounts.Any(e => e.Id == accountId);
        }

        public bool VerifyBalance(Account account, double value)
        {
          return account.VerifyBalanceWithDraw(value);
        }


        public TransferDataViewModel Transfer(Account origin, Account destination, double value)
        {
            bool  hasBalance = origin.VerifyBalanceWithDraw(value);

            if (!hasBalance) return new TransferDataViewModel {Operation = false};

            origin.setBalance(value, Enums.OperationsEnum.withdraw);
            _context.Accounts.Add(origin).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            
            destination.setBalance(value, Enums.OperationsEnum.Deposit);
            _context.Accounts.Add(origin).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _context.SaveChanges();

            return new TransferDataViewModel
            {
                Operation = true,
                Origin = new TransferViewModelData
                {
                    Id = origin.Id,
                    Balance = origin.Balance
                },
                Destination = new TransferViewModelData
                {
                    Balance = destination.Balance,
                    Id = destination.Id
                }

            };
        }

        public void Reset()
        {
            _context.Accounts.RemoveRange(_context.Accounts.ToList());
            _context.SaveChanges();

        }

        
    }
}
