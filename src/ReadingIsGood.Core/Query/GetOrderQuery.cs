using ReadingIsGood.Core.CQRS;
using ReadingIsGood.Core.Response;

namespace ReadingIsGood.Core.Query
{
    public class GetOrderQuery : IQuery<OrderResponse>
    {
        public string OrderId { get; set; } = string.Empty;

        public string CustomerId { get; set; } = string.Empty;
    }
}
