using ReadingIsGood.Core.Entities;
using ReadingIsGood.Core.Repositories.Abstractions;
using ReadingIsGood.MongoDB.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReadingIsGood.Core.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IGenericRepository<Customer, string> genericRepository;
        public CustomerRepository(
            IGenericRepository<Customer, string> genericRepository)
        {
            this.genericRepository = genericRepository;
        }

        public async Task<IReadOnlyList<Customer>> GetCustomersAsync(int limit, int offset)
        {
            return await this.genericRepository
                                .Query()
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();
        }

        public async Task<Customer?> GetCustomerAsync(string id)
        {
            return await this.genericRepository
                                .GetByIdAsync(id);
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            await this.genericRepository
                                .AddOneAsync(customer);
        }
    }
}
