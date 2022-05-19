using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;
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

            //return StatusCode(200);

            return new MessageResult("OK");


        }

        public class MessageResult : IActionResult
        {
            private readonly string _message;

            public MessageResult(string message)
            {
                _message = message;
            }

            public async Task ExecuteResultAsync(ActionContext context)
            {                
                context.HttpContext.Response.StatusCode = 200;

                var myByteArray = Encoding.UTF8.GetBytes(_message);
                await context.HttpContext.Response.Body.WriteAsync(myByteArray, 0, myByteArray.Length);
                await context.HttpContext.Response.Body.FlushAsync();
            }

        }
    }
}
