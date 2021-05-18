using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ReadingIsGood.Api.Controllers
{
    [Route("customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> logger;

        public CustomerController(ILogger<CustomerController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            this.logger.LogInformation("created c");
            return Ok();
        }
    }
}
