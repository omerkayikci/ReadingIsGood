using ReadingIsGood.Core.CQRS;
using ReadingIsGood.Core.Response;

namespace ReadingIsGood.Core.Query
{
    public class GetCustomerQuery : IQuery<CustomerResponse>
    {
        public string? customerId { get; set; }
    }
}
