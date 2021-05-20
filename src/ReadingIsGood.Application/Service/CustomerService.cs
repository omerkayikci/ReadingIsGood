using Microsoft.Extensions.Logging;
using ReadingIsGood.Application.Extensions;
using ReadingIsGood.Common.ExceptionHandling;
using ReadingIsGood.Core.Entities;
using ReadingIsGood.Core.Query;
using ReadingIsGood.Core.Repositories.Abstractions;
using ReadingIsGood.Core.Request;
using ReadingIsGood.Core.Response;
using ReadingIsGood.Core.Services.Abstractions;
using ReadingIsGood.MongoDB.Abstractions;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ReadingIsGood.Application.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IUserRepository userRepository;
        public CustomerService(ICustomerRepository customerRepository,
            IUserRepository userRepository)
        {
            this.customerRepository = customerRepository;
            this.userRepository = userRepository;
        }

        public async Task<string> AddCustomerAsync(CustomerRequest request)
        {
            Customer customer = request.ToCustomer();

            await this.userRepository.AddUserAsync(request.ToCustomerUser(customer.Id));
            await this.customerRepository.AddCustomerAsync(customer);

            return customer.Id;
        }

        public async Task<CustomerResponse> GetCustomerAsync(GetCustomerQuery request)
        {
            Customer? customer = await this.customerRepository.GetCustomerAsync(request.customerId!);

            if (customer == null)
            {
                throw new ReadingIsGoodException($"The customer not found by customer id. Id: {request.customerId}", HttpStatusCode.NotFound, logLevel: LogLevel.Warning);
            }

            return customer.ToCustomerResponse();
        }

        public async Task<IEnumerable<CustomersResponse>> GetCustomersAsync(GetCustomersQuery request)
        {
            if (request.Limit == null || request.Limit <= 0)
            {
                request.Limit = 1000;
            }

            if (request.Offset == null || request.Offset < 0)
            {
                request.Offset = 0;
            }

            IEnumerable<Customer> response = await this.customerRepository.GetCustomersAsync((int)request.Limit, (int)request.Offset);

            return response.ToCustomersRespnse();
        }
    }
}
