using ReadingIsGood.Common.Enums;
using ReadingIsGood.Core.Query;
using ReadingIsGood.Core.Request;
using ReadingIsGood.Core.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReadingIsGood.Core.Services.Abstractions
{
    public interface IOrderService : IApplicationService
    {
        Task<string> CreateOrderAsync(CreateOrderRequest orderRequest);
        Task<OrderResponse> GetOrderByIdAsync(GetOrderQuery getOrderrequest);
        Task<IReadOnlyList<AllOrderResponse>> GetAllOrderByCustomerAsync(GetAllOrderQuery getAllOrderRequest);
        Task<string> UpdateOrderStatusAsync(OrderStatusUpdateRequest orderStatusUpdateRequest);
    }
}
