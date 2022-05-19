using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Transactions.Persistence.Repositories;

namespace Transactions.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BalanceController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public BalanceController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet]

        public IActionResult Get(int account_id)
        {
            if (account_id == 0) return BadRequest();

            var account = _accountRepository.GetById(account_id);

            if (account == null) return NotFound();

            return Ok(new {account.Balance});

        }


    }
}
