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

        public IActionResult Get(string account_id)
        {
            if (string.IsNullOrEmpty(account_id)) return BadRequest();

            var account = _accountRepository.GetById(account_id);

            if (account == null) return NotFound(0);

            return Ok(account.Balance);

        }


    }
}
