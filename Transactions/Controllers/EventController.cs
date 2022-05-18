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


            return CreatedAtRoute(null,  new { destination = new { id = account.Id, account.Balance } });
        }

        private object GetById()
        {
            throw new NotImplementedException();
        }
    }
}
