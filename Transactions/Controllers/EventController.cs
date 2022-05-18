using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Transactions.Persistence.Models;
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
        public IActionResult Post([FromBody] CreateAccountViewModel model)
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
                }
                else
                {
                    var objAccount = new Account();

                    double value = model.Amount;

                    objAccount.Id = model.Destination;
                    objAccount.setBalance(value, Enums.OperationsEnum.Deposit);

                    account = _accountRepository.Create(objAccount);

                }

                return CreatedAtRoute(null, new { destination = new { id = account.Id, account.Balance } });
                //return CreatedAtRoute("balance", new { id = account.Id }, new { destination = new { id = account.Id, account.Balance } });

                #endregion
            }
            else if (model.Type.Equals("withdraw"))
            {
                bool accountExist = _accountRepository.AccountExist(model.Origin);

                Account account;

                if (!accountExist)
                {
                    return NotFound();
                }
                else
                {
                    account = _accountRepository.GetById(model.Origin);

                    bool verifyBalance = _accountRepository.VerifyBalance(account, model.Amount);

                    if (!verifyBalance) return BadRequest(new { origin = account.Id, message = "insufficient funds" });
                    
                    _accountRepository.Withdraw(account, model.Amount);
                }

                 return CreatedAtRoute(null, new { origin = new { id = account.Id, account.Balance } });
                // return CreatedAtRoute("balance", new {id = account.Id}, new { origin = new { id = account.Id, account.Balance } });
            }

            return Ok();

        }

    }
}
