using ReadingIsGood.Application.Mediator.Query;
using ReadingIsGood.Core.Query;
using ReadingIsGood.Core.Response;
using ReadingIsGood.Core.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReadingIsGood.Application.Handlers.Customer
{
    public class GetCustomersQueryHandler : IQueryHandler<GetCustomersQuery, IEnumerable<CustomersResponse>>
    {
        private readonly ICustomerService customerService;
        public GetCustomersQueryHandler(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public async Task<IEnumerable<CustomersResponse>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            return await this.customerService.GetCustomersAsync(request);
        }
    }
}
