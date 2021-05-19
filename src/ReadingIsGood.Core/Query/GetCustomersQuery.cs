using ReadingIsGood.Core.CQRS;
using ReadingIsGood.Core.Response;

namespace ReadingIsGood.Core.Query
{
    public class GetCustomersQuery : IQuery<CustomersResponse>
    {
        public int? Limit { get; set; }

        public int? Offset { get; set; }
    }
}
