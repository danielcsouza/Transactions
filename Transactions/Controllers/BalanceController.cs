using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Transactions.Persistence.Repositories;

namespace Transactions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BalanceController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public BalanceController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet]
        public IActionResult Get(int accountId)
        {
            if (accountId == 0) return BadRequest();

            var account = _accountRepository.Get(accountId);

            if (account == null) return NotFound();

            return Ok(new {account.Balance});

        }


    }
}
