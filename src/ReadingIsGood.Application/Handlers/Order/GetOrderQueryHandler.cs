using ReadingIsGood.Application.Mediator.Query;
using ReadingIsGood.Core.Query;
using ReadingIsGood.Core.Response;
using ReadingIsGood.Core.Services.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace ReadingIsGood.Application.Handlers.Order
{
    public class GetOrderQueryHandler : IQueryHandler<GetOrderQuery, OrderResponse>
    {
        private readonly IOrderService orderService;
        public GetOrderQueryHandler(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public async Task<OrderResponse> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            return await this.orderService.GetOrderByIdAsync(request);
        }
    }
}
