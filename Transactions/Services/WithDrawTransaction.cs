using Transactions.Domains;
using Transactions.Persistence.Repositories;
using Transactions.Services.ViewModels;

namespace Transactions.Services
{
    public class WithDrawTransaction : ITransaction
    {
        private readonly IAccountRepository _accountRepository;

        public WithDrawTransaction(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public ResultViewModel ExecuteTransaction(string? origin, string? destination, double value)
        {
            bool accountExist = _accountRepository.AccountExist(origin);

            Account account;

            if (!accountExist)
            {
                return new ResultViewModel
                {
                    Success = false,
                    Message = string.Empty
                };
            }

            account = _accountRepository.GetById(origin);

            bool verifyBalance = _accountRepository.VerifyBalance(account, value);

            if (!verifyBalance)
            {
                return new ResultViewModel
                {
                    Success = false,
                    Message = "insufficient funds"
                };

            }

            _accountRepository.Withdraw(account, value);

            return new ResultViewModel
            {
                Success = true,
                Origin = account.Id,
                Balance = account.Balance
            };

        }

    }
}
