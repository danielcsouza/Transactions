using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Transactions.Extensions
{
    public class MessageResultExtension : IActionResult
    {
        private readonly string _message;

        public MessageResultExtension(string message)
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
