using Microsoft.AspNetCore.Mvc;
using Transactions.Domains;
using Transactions.Persistence.Repositories;
using Transactions.Persistence.ViewModels;
using Transactions.Services;

namespace Transactions.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransaction _transaction;

        public EventController(IAccountRepository accountRepository, ITransaction transaction)
        {
            _accountRepository = accountRepository;
            _transaction = transaction;
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
                var operation = _transaction.Deposit(model.Destination, model.Amount);

                if (operation.Success)
                {
                    return CreatedAtRoute(null, new { destination = new { id = operation.Destination, operation.Balance } });
                }

                return BadRequest(0);

            }
            else if (model.Type.Equals("withdraw"))
            {
                var operation = _transaction.WhitDraw(model.Origin, model.Amount);

                if (operation.Success)
                {
                    return CreatedAtRoute(null, new { destination = new { id = operation.Origin, operation.Balance } });
                }

                if (!string.IsNullOrEmpty(operation.Messages))
                {
                    return BadRequest(operation.Messages);
                }

                return NotFound(0);

            }
            else if (model.Type.Equals("transfer"))
            {
                var operation = _transaction.Transfer(model.Origin, model.Destination, model.Amount);

                if (operation.Operation)
                {
                    return CreatedAtRoute(null, operation);
                }

                if (!string.IsNullOrEmpty(operation.Message))
                {
                    return BadRequest(operation.Message);
                }

                return NotFound(0);
            }

            return BadRequest(0);
        }

    }
}
