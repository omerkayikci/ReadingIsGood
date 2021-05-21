using ReadingIsGood.Application.Mediator.Command;
using ReadingIsGood.Core.Request;
using ReadingIsGood.Core.Services.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace ReadingIsGood.Application.Handlers.Order
{
    public class CreateOrderRequestCommandHandler : ICommandHandler<CreateOrderRequest, string>
    {
        private readonly IOrderService orderService;
        public CreateOrderRequestCommandHandler(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public async Task<string> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            return await this.orderService.CreateOrderAsync(request);
        }
    }
}
