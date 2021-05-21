using ReadingIsGood.Core.CQRS;
using ReadingIsGood.Core.Response;
using System.Collections.Generic;

namespace ReadingIsGood.Core.Query
{
    public class GetAllOrderQuery : IQuery<IReadOnlyList<AllOrderResponse>>
    {
        public string CustomerId { get; set; } = string.Empty;
        public int? Limit { get; set; }
        public int? Offset { get; set; }
    }
}
