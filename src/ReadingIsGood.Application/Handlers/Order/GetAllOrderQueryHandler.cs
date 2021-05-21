using ReadingIsGood.Application.Mediator.Query;
using ReadingIsGood.Core.Query;
using ReadingIsGood.Core.Response;
using ReadingIsGood.Core.Services.Abstractions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ReadingIsGood.Application.Handlers.Order
{
    public class GetAllOrderQueryHandler : IQueryHandler<GetAllOrderQuery, IReadOnlyList<AllOrderResponse>>
    {
        private readonly IOrderService orderService;
        public GetAllOrderQueryHandler(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public async Task<IReadOnlyList<AllOrderResponse>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
        {
            return await this.orderService.GetAllOrderByCustomerAsync(request);
        }
    }
}
