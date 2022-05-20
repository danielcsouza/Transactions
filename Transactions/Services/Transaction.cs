using Transactions.Domains;
using Transactions.Persistence.Repositories;
using Transactions.Services.ViewModels;

namespace Transactions.Services
{
    public class Transaction : ITransaction
    {
        private readonly IAccountRepository _accountRepository;

        public Transaction(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public ResultDepositViewModel Deposit(string? destination, double value)
        {

            if (string.IsNullOrEmpty(destination))
            {
                return new ResultDepositViewModel
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

                return new ResultDepositViewModel
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

            return new ResultDepositViewModel
            {
                Success = true,
                Balance = account.Balance,
                Destination = account.Id
            };

        }

        public ResultWithDrawViewModel WhitDraw(string origin, double value)
        {
            bool accountExist = _accountRepository.AccountExist(origin);

            Account account;

            if (!accountExist)
            {
                //return NotFound(0);
                return new ResultWithDrawViewModel
                {
                    Success = false,
                    Messages = string.Empty
                };
            }

            account = _accountRepository.GetById(origin);

            bool verifyBalance = _accountRepository.VerifyBalance(account, value);

            if (!verifyBalance)
            {
                return new ResultWithDrawViewModel
                {
                    Success = false,
                    Messages = "insufficient funds"
                };

                //return BadRequest(new { origin = account.Id.ToString(), message = "insufficient funds" });

            }

            _accountRepository.Withdraw(account, value);

            return new ResultWithDrawViewModel
            {
                Success = true,
                Origin = account.Id,
                Balance = account.Balance
            };

            //return CreatedAtRoute(null, new { origin = new { id = account.Id.ToString(), account.Balance } });

        }

        public TransferDataViewModel Transfer(string origin, string destination, double value)
        {
           
            bool originExist = _accountRepository.AccountExist(origin);
            bool destinationExist = _accountRepository.AccountExist(destination);

            Account originAccount;
            Account destinationAccount;
            TransferDataViewModel returnOperation;

            if (!originExist)
            {
                return new TransferDataViewModel 
                {
                    Operation = false
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
                    return returnOperation;
                }

                return new TransferDataViewModel
                {
                    Operation = false,
                    Message = "insuficient funds"
                };

            }

            return new TransferDataViewModel
            {
                Operation = false
            };

            
        }

    
    
    }
}
