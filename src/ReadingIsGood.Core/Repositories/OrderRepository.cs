using MongoDB.Driver;
using ReadingIsGood.Core.Data.Abstractions;
using ReadingIsGood.Core.Entities;
using ReadingIsGood.Core.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReadingIsGood.Core.Repositories
{
    public class OrderRepository : IOrderRespository
    {
        private readonly IReadingIsGoodContext _context;

        public OrderRepository(IReadingIsGoodContext _readingIsGoodContext)
        {
            _context = _readingIsGoodContext ?? throw new ArgumentNullException(nameof(_readingIsGoodContext));
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _context
                            .Order
                            .Find(p => true)
                            .ToListAsync();
        }

        public async Task<Order> GetOrderAsync(string id)
        {
            return await _context
                            .Order
                            .Find(p => p.Id == id)
                            .FirstOrDefaultAsync();
        }
    }
}
