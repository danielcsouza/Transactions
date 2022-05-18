using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Transactions.Controllers
{
    [Route("reset")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public IActionResult Reset()
        {
            return Ok();
        }
    }
}
