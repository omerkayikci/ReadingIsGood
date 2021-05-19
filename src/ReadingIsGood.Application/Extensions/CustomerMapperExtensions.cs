using ReadingIsGood.Core.Entities;
using ReadingIsGood.Core.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
