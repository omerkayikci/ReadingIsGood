using ReadingIsGood.Common.Enums;
using ReadingIsGood.Core.Entities;
using ReadingIsGood.MongoDB.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReadingIsGood.Core.Repositories.Abstractions
{
    public interface IOrderRespository
    {
        Task CreateOrderAsync(Order order);
        Task<Order?> GetOrderByIdAsync(string orderId, string customerId);
        Task<IReadOnlyList<Order>> GetOrdersAsync(string customerId, int limit, int offset);
        Task<string> UpdateOrderStatusAsync(Order order);
        Task<ITransactionScope> BeginTransactionScopeAsync();
    }
}
