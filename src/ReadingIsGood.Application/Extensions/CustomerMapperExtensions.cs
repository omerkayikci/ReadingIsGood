using ReadingIsGood.Core.Entities;
using ReadingIsGood.Core.Request;
using ReadingIsGood.Core.Response;
using System.Collections.Generic;
using System.Linq;

namespace ReadingIsGood.Application.Extensions
{
    public static class CustomerMapperExtensions
    {
        public static CustomerResponse ToCustomerResponse(this Customer response)
        {
            return new CustomerResponse
            {
                Address = response.Address,
                Email = response.Email,
                Name = response.Name,
                PhoneNumber = response.PhoneNumber
            };
        }

        public static IEnumerable<CustomersResponse> ToCustomersRespnse(this IEnumerable<Customer> response)
        {
            return response.Select(r => new CustomersResponse
            {
                Id = r.Id,
                Name = r.Name
            });
        }

        public static Customer ToCustomer(this CustomerRequest request)
        {
            return new Customer
            {
                Name = request.Name,
                Address = request.Address,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
            };
        }

        public static User ToCustomerUser(this CustomerRequest request, string customerId)
        {
            return new User
            {
                CustomerId = customerId,
                Password = request.Password,
                Username = request.Username
            };
        }
    }
}
