using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReadingIsGood.Application.Mediator.Query;
using ReadingIsGood.Common.Models;
using ReadingIsGood.Core.Query;
using ReadingIsGood.Core.Response;
using System;
using System.Threading.Tasks;

namespace ReadingIsGood.Api.Controllers
{
    [Route("customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IQueryProcessor queryProcessor;

        public CustomerController(
            ILogger<CustomerController> logger,
            IQueryProcessor queryProcessor)
        {
            this.queryProcessor = queryProcessor;
        }

        [Route("{customerId}")]
        [HttpGet]
        public async Task<ActionResult<ApiResponse<CustomerResponse>>> GetCustomers(Guid customerId)
        {
            CustomerResponse response = await queryProcessor.ProcessAsync(new GetCustomerQuery() { customerId = customerId });
            return Ok(new ApiResponse<CustomerResponse> { Data = response });
        }
    }
}
