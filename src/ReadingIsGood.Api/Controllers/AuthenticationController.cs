using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingIsGood.Application.Mediator.Command;
using ReadingIsGood.Common.Models;
using ReadingIsGood.Core.Request;
using System.Threading.Tasks;

namespace ReadingIsGood.Api.Controllers
{
    [AllowAnonymous()]
    [Route("token")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ICommandSender commandSender;

        public AuthenticationController(
            ICommandSender commandSender)
        {
            this.commandSender = commandSender;
        }

        [HttpPost("create")]
        public async Task<ActionResult<ApiResponse<string>>> GetToken([FromBody] AuthRequest request)
        {
            string response = await this.commandSender.SendAsync<string>(request);
            return Ok(new ApiResponse<string> { Data = response.ToString() });

        }
    }
}
