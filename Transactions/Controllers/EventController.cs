using Microsoft.AspNetCore.Mvc;
using Transactions.Persistence.Repositories;
using Transactions.Persistence.ViewModels;
using Transactions.Services;
using Transactions.Services.ViewModels;

namespace Transactions.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly ITransaction _transaction;
        private readonly IAccountRepository _accountRepository;

        public EventController(ITransaction transaction, IAccountRepository accountRepository)
        {
            _transaction = transaction;
            _accountRepository = accountRepository;
        }

        [HttpPost]
        public IActionResult Post([FromBody] AccountViewModel model)
        {
            Transaction transaction = new Transaction();
            ResultViewModel returnTransaction = new ResultViewModel();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.Type.Equals("deposit"))
            {
                transaction.DefineStrategy(new DepositTransaction(_accountRepository));

                returnTransaction = transaction.ExecuteTransaction(null, model.Destination, model.Amount);

                if (returnTransaction.Success)
                {
                    return CreatedAtRoute(null, new { destination = new { id = returnTransaction.Destination, returnTransaction.Balance } });
                }

                return BadRequest(0);

            }
            else if (model.Type.Equals("withdraw"))
            {
                transaction.DefineStrategy(new WithDrawTransaction(_accountRepository));

                returnTransaction = transaction.ExecuteTransaction(model.Origin, null, model.Amount);

                if (returnTransaction.Success)
                {
                    return CreatedAtRoute(null, new { origin = new { id = returnTransaction.Origin, returnTransaction.Balance } });
                }

                if (!string.IsNullOrEmpty(returnTransaction.Message))
                {
                    return BadRequest(returnTransaction.Message);
                }

                return NotFound(0);

            }
            else if (model.Type.Equals("transfer"))
            {
                transaction.DefineStrategy(new TransferTransaction(_accountRepository));

                returnTransaction = transaction.ExecuteTransaction(model.Origin, model.Destination, model.Amount);

                if (returnTransaction.Success)
                {
                    return CreatedAtRoute(null, returnTransaction.TransactionResult);
                }

                if (!string.IsNullOrEmpty(returnTransaction.Message))
                {
                    return BadRequest(returnTransaction.Message);
                }

                return NotFound(0);
            }

            return BadRequest(0);
        }

    }
}
