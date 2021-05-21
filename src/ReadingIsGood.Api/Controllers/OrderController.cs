using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    [Route("orders")]
    [Authorize()]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IQueryProcessor queryProcessor;
        private readonly ICommandSender commandSender;

        public OrderController(
            IQueryProcessor queryProcessor,
            ICommandSender commandSender)
        {
            this.queryProcessor = queryProcessor;
            this.commandSender = commandSender;
        }

        /// <summary>
        /// All Orders
        /// </summary>
        /// <param name="request">Limit&Offset</param>
        /// <returns></returns>

        [Route("allOrders")]
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IReadOnlyList<AllOrderResponse>>>> GetAllOrders([FromQuery] GetAllOrderQuery request)
        {
            IReadOnlyList<AllOrderResponse> response = await queryProcessor.ProcessAsync(request);
            return Ok(new ApiResponse<IReadOnlyList<AllOrderResponse>> { Data = response });
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<OrderResponse>>> GetOrder([FromQuery] GetOrderQuery request)
        {
            OrderResponse response = await queryProcessor.ProcessAsync(request);
            return Ok(new ApiResponse<OrderResponse> { Data = response });
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<string>>> CreateOrderAsync([FromBody] CreateOrderRequest request)
        {
            string response = await this.commandSender.SendAsync<string>(request);
            return Ok(new ApiResponse<string> { Data = response.ToString() });
        }

        [Route("updateOrder")]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<string>>> UpdateOrderStatusAsync([FromBody] OrderStatusUpdateRequest request)
        {
            string response = await this.commandSender.SendAsync<string>(request);
            return Ok(new ApiResponse<string> { Data = response.ToString() });
        }
    }
}
