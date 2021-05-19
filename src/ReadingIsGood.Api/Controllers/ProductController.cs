using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReadingIsGood.Application.Mediator.Command;
using ReadingIsGood.Application.Mediator.Query;
using ReadingIsGood.Common.Models;
using ReadingIsGood.Core.Query;
using ReadingIsGood.Core.Request;
using ReadingIsGood.Core.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingIsGood.Api.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IQueryProcessor queryProcessor;
        private readonly ICommandSender commandSender;
        public ProductController(
            IQueryProcessor queryProcessor,
            ICommandSender commandSender)
        {
            this.queryProcessor = queryProcessor;
            this.commandSender = commandSender;
        }

        [Route("{productId}")]
        [HttpGet]
        public async Task<ActionResult<ApiResponse<ProductResponse>>> GetProductAsync(string productId)
        {
            ProductResponse response = await queryProcessor.ProcessAsync(new GetProductQuery() { productId = productId });
            return Ok(new ApiResponse<ProductResponse> { Data = response });
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<string>>> AddProductAsync([FromBody] ProductRequest request)
        {
            string response = await commandSender.SendAsync<string>(request);
            return Ok(new ApiResponse<string> { Data = response });
        }

        [Route("updateStock")]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<string>>> AddProductStockUpdateAsync([FromBody] UpdateStockRequest request)
        {
            string response = await commandSender.SendAsync<string>(request);
            return Ok(new ApiResponse<string> { Data = response });
        }
    }
}
