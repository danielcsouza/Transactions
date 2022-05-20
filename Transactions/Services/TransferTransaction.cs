using Transactions.Domains;
using Transactions.Persistence.Repositories;
using Transactions.Services.ViewModels;

namespace Transactions.Services
{
    public class TransferTransaction : ITransaction
    {
        private readonly IAccountRepository _accountRepository;

        public TransferTransaction(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public ResultViewModel ExecuteTransaction(string? origin, string? destination, double value)
        {

            bool originExist = _accountRepository.AccountExist(origin);
            bool destinationExist = _accountRepository.AccountExist(destination);

            Account originAccount;
            Account destinationAccount;

            TransferDataViewModel returnOperation;

            if (!originExist)
            {
                return new ResultViewModel
                {
                    Success = false
                };
            }

            if (!destinationExist)
            {
                var objAccount = new Account();

                objAccount.Id = destination;
                destinationAccount = _accountRepository.Create(objAccount);
                originAccount = _accountRepository.GetById(origin);

                returnOperation = _accountRepository.Transfer(originAccount, destinationAccount, value);


                if (returnOperation.Operation)
                {
                    return new ResultViewModel
                    {
                        Success = true,
                        TransactionResult = returnOperation
                    };

                }

                return new ResultViewModel
                {
                    Success = false,
                    Message = "insuficient funds"
                };

            }

            return new ResultViewModel
            {
                Success = false
            };
        }
    }
}
