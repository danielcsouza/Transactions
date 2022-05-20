using Microsoft.AspNetCore.Mvc;
using System.Text;
using Transactions.Extensions;
using Transactions.Persistence.Repositories;

namespace Transactions.Controllers
{
    [Route("reset")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        [HttpPost]
        [Route("")]
        public IActionResult Reset()
        {
            _accountRepository.Reset();

            return new MessageResultExtension("OK");

        }

        
    }
}
