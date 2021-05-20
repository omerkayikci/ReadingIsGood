using ReadingIsGood.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReadingIsGood.Core.Repositories.Abstractions
{
    public interface ICustomerRepository
    {
        Task<IReadOnlyList<Customer>> GetCustomersAsync(int limit, int offset);
        Task<Customer?> GetCustomerAsync(string id);
        Task AddCustomerAsync(Customer customer);
    }
}
