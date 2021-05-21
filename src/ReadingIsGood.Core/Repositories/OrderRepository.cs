using ReadingIsGood.Common.Enums;
using ReadingIsGood.Core.Entities;
using ReadingIsGood.Core.Repositories.Abstractions;
using ReadingIsGood.MongoDB.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReadingIsGood.Core.Repositories
{
    public class OrderRepository : IOrderRespository
    {
        private readonly IGenericRepository<Order, string> genericRepository;
        public OrderRepository(
            IGenericRepository<Order, string> genericRepository)
        {
            this.genericRepository = genericRepository;
        }

        public async Task CreateOrderAsync(Order order)
        {
            await this.genericRepository
                                .AddOneAsync(order);
        }

        public async Task<Order?> GetOrderByIdAsync(string orderId, string customerId)
        {
            return await this.genericRepository
                              .Query()
                              .Where(x => x.Id == orderId && x.CustomerId == customerId)
                              .FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<Order>> GetOrdersAsync(string customerId, int limit, int offset)
        {
            IReadOnlyList<Order>? orders = await this.genericRepository
                                .Query()
                                .Where(x => x.CustomerId == customerId)
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();

            return orders != null ? orders : new List<Order>();
        }

        public async Task<string> UpdateOrderStatusAsync(Order order)
        {
            await this.genericRepository.UpdateAsync(order);

            return order.Id;
        }
    }
}
