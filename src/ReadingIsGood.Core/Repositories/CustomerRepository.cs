using MongoDB.Driver;
using ReadingIsGood.Core.Data.Abstractions;
using ReadingIsGood.Core.Entities;
using ReadingIsGood.Core.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReadingIsGood.Core.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IReadingIsGoodContext _context;

        public CustomerRepository(IReadingIsGoodContext _readingIsGoodContext)
        {
            _context = _readingIsGoodContext ?? throw new ArgumentNullException(nameof(_readingIsGoodContext));
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return await _context
                            .Customer
                            .Find(p => true)
                            .ToListAsync();
        }

        public async Task<Customer?> GetCustomerAsync(Guid id)
        {
            return await _context
                            .Customer
                            .Find(p => p.CustomerId == id)
                            .FirstOrDefaultAsync();
        }
    }
}
