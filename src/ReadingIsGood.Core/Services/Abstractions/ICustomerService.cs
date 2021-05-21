using ReadingIsGood.Core.Query;
using ReadingIsGood.Core.Request;
using ReadingIsGood.Core.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReadingIsGood.Core.Services.Abstractions
{
    public interface ICustomerService : IApplicationService
    {
        Task<CustomerResponse> GetCustomerAsync(GetCustomerQuery request);

        Task<IEnumerable<CustomersResponse>> GetCustomersAsync(GetCustomersQuery request);

        Task<string> AddCustomerAsync(CustomerRequest request);

    }
}
