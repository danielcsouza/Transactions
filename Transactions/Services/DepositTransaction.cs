using Transactions.Domains;
using Transactions.Persistence.Repositories;
using Transactions.Services.ViewModels;

namespace Transactions.Services
{
    public class DepositTransaction : ITransaction
    {
        private readonly IAccountRepository _accountRepository;

        public DepositTransaction(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public ResultViewModel ExecuteTransaction(string? origin, string? destination, double value)
        {
            if (string.IsNullOrEmpty(destination))
            {
                return new ResultViewModel
                {
                    Success = false
                };

            }

            bool accountExist = _accountRepository.AccountExist(destination);

            Account account;

            if (accountExist)
            {
                account = _accountRepository.GetById(destination);

                _accountRepository.Deposit(account, value);

                return new ResultViewModel
                {
                    Success = true,
                    Balance = account.Balance,
                    Destination = account.Id
                };

            }

            var objAccount = new Account();

            objAccount.Id = destination;
            objAccount.setBalance(value, Enums.OperationsEnum.Deposit);

            account = _accountRepository.Create(objAccount);

            return new ResultViewModel
            {
                Success = true,
                Balance = account.Balance,
                Destination = account.Id
            };
        }
    }
}
