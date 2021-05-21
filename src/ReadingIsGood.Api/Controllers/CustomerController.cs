using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReadingIsGood.Application.Mediator.Command;
using ReadingIsGood.Application.Mediator.Query;
using ReadingIsGood.Common.Models;
using ReadingIsGood.Core.Query;
using ReadingIsGood.Core.Request;
using ReadingIsGood.Core.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReadingIsGood.Api.Controllers
{
    [Route("customers")]
    [Authorize()]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IQueryProcessor queryProcessor;
        private readonly ICommandSender commandSender;

        public CustomerController(
            ILogger<CustomerController> logger,
            IQueryProcessor queryProcessor,
            ICommandSender commandSender)
        {
            this.queryProcessor = queryProcessor;
            this.commandSender = commandSender;
        }

        /// <summary>
        /// All Customers
        /// </summary>
        /// <param name="request">Limit&Offset</param>
        /// <returns></returns>

        [AllowAnonymous()]
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<CustomersResponse>>>> GetCustomers([FromQuery] GetCustomersQuery request)
        {
            IEnumerable<CustomersResponse> response = await queryProcessor.ProcessAsync(request);
            return Ok(new ApiResponse<IEnumerable<CustomersResponse>> { Data = response });
        }

        [Route("{customerId}")]
        [HttpGet]
        public async Task<ActionResult<ApiResponse<CustomerResponse>>> GetCustomer(string customerId)
        {
            CustomerResponse response = await queryProcessor.ProcessAsync(new GetCustomerQuery() { customerId = customerId });
            return Ok(new ApiResponse<CustomerResponse> { Data = response });
        }

        [AllowAnonymous()]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<string>>> CreateCustomerAsync([FromBody] CustomerRequest request)
        {
            string response = await this.commandSender.SendAsync<string>(request);
            return Ok(new ApiResponse<string> { Data = response.ToString() });
        }
    }
}
