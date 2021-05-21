using ReadingIsGood.Application.Mediator.Command;
using ReadingIsGood.Core.Request;
using ReadingIsGood.Core.Services.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace ReadingIsGood.Application.Handlers.Order
{
    public class OrderStatusUpdateRequestCommandHandler : ICommandHandler<OrderStatusUpdateRequest, string>
    {
        private readonly IOrderService orderService;
        public OrderStatusUpdateRequestCommandHandler(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public async Task<string> Handle(OrderStatusUpdateRequest request, CancellationToken cancellationToken)
        {
            return await this.orderService.UpdateOrderStatusAsync(request);
        }
    }
}
