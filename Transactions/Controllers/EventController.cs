using Microsoft.AspNetCore.Mvc;
using Transactions.Domains;
using Transactions.Persistence.Repositories;
using Transactions.Persistence.ViewModels;

namespace Transactions.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public EventController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        [HttpPost]
        public IActionResult Post([FromBody] AccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (model.Type.Equals("deposit"))
            {
                #region Deposit

                bool accountExist = _accountRepository.AccountExist(model.Destination);

                Account account;

                if (accountExist)
                {
                    account = _accountRepository.GetById(model.Destination);

                    _accountRepository.Deposit(account, model.Amount);

                    return CreatedAtRoute(null, new { destination = new { id = account.Id.ToString(), account.Balance } });
                }

                if (!string.IsNullOrEmpty(model.Destination))
                {
                    var objAccount = new Account();

                    double value = model.Amount;
                    objAccount.Id = model.Destination;
                    objAccount.setBalance(value, Enums.OperationsEnum.Deposit);

                    account = _accountRepository.Create(objAccount);

                    return CreatedAtRoute(null, new { destination = new { id = account.Id.ToString(), account.Balance } });
                }

                return BadRequest(0);

                #endregion
            }
            else if (model.Type.Equals("withdraw"))
            {
                #region Withdraw
                bool accountExist = _accountRepository.AccountExist(model.Origin);

                Account account;

                if (!accountExist)
                {
                    return NotFound(0);
                }

                account = _accountRepository.GetById(model.Origin);

                bool verifyBalance = _accountRepository.VerifyBalance(account, model.Amount);

                if (!verifyBalance) return BadRequest(new { origin = account.Id.ToString(), message = "insufficient funds" });

                _accountRepository.Withdraw(account, model.Amount);

                return CreatedAtRoute(null, new { origin = new { id = account.Id.ToString(), account.Balance } });

                #endregion
            }
            else if (model.Type.Equals("transfer"))
            {
                #region Transfer
                bool originExist = _accountRepository.AccountExist(model.Origin);
                bool destinationExist = _accountRepository.AccountExist(model.Destination);

                Account origin;
                Account destination;
                TransferDataViewModel returnOperation;

                if (!originExist)
                {
                    return NotFound(0);
                }

                if (!destinationExist)
                {
                    if (string.IsNullOrEmpty(model.Destination))
                    {
                        return BadRequest(0);
                    }

                    var objAccount = new Account();

                    objAccount.Id = model.Destination;
                    destination = _accountRepository.Create(objAccount);
                    origin = _accountRepository.GetById(model.Origin);

                    returnOperation = _accountRepository.Transfer(origin, destination, model.Amount);

                    return returnOperation.Operation == false ?
                            BadRequest(new { message = "insuficient funds" }) :
                            new CreatedAtRouteResult(null, returnOperation);
                }


                return BadRequest(0);

                #endregion
            }

            return BadRequest(0);

        }

    }
}
