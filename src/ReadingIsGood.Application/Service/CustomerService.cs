using Microsoft.Extensions.Logging;
using ReadingIsGood.Application.Extensions;
using ReadingIsGood.Common.ExceptionHandling;
using ReadingIsGood.Core.Entities;
using ReadingIsGood.Core.Query;
using ReadingIsGood.Core.Repositories.Abstractions;
using ReadingIsGood.Core.Response;
using ReadingIsGood.Core.Services.Abstractions;
using System.Net;
using System.Threading.Tasks;

namespace ReadingIsGood.Application.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<CustomerResponse> GetCustomerAsync(GetCustomerQuery request)
        {
            Customer? customer = await this.customerRepository.GetCustomerAsync(request.customerId);

            if (customer == null)
            {
                throw new ReadingIsGoodException($"The sas operation was not found according to the requested id. Id: {request.customerId}", HttpStatusCode.NotFound, logLevel: LogLevel.Warning);
            }

            return customer.ToCustomerResponse();
        }
    }
}
