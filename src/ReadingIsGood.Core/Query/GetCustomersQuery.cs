using ReadingIsGood.Core.CQRS;
using ReadingIsGood.Core.Response;
using System.Collections.Generic;

namespace ReadingIsGood.Core.Query
{
    public class GetCustomersQuery : IQuery<IEnumerable<CustomersResponse>>
    {
        public int? Limit { get; set; }

        public int? Offset { get; set; }
    }
}
