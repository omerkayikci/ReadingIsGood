using ReadingIsGood.Application.Mediator.Command;
using ReadingIsGood.Core.Request;
using ReadingIsGood.Core.Services.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace ReadingIsGood.Application.Handlers.Authentication
{
    public class CreateTokenCommandHandler : ICommandHandler<AuthRequest, string>
    {
        private readonly IAuthenticationService authenticationService;
        public CreateTokenCommandHandler(
            IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        public async Task<string> Handle(AuthRequest request, CancellationToken cancellationToken)
        {
            return await this.authenticationService.GenerateToken(request);
        }
    }
}
