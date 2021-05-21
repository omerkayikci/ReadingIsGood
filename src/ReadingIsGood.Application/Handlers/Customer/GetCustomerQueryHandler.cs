using ReadingIsGood.Application.Mediator.Query;
using ReadingIsGood.Core.Query;
using ReadingIsGood.Core.Response;
using ReadingIsGood.Core.Services.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace ReadingIsGood.Application.Handlers.Customer
{
    public class GetCustomerQueryHandler : IQueryHandler<GetCustomerQuery, CustomerResponse>
    {
        private readonly ICustomerService customerService;
        public GetCustomerQueryHandler(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public Task<CustomerResponse> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            return customerService.GetCustomerAsync(request);
        }
    }
}
