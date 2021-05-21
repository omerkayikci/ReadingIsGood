using ReadingIsGood.Core.CQRS;
using System.Collections.Generic;

namespace ReadingIsGood.Core.Request
{
    public class CreateOrderRequest : ICommand<string>
    {
        public string? OrderNote { get; set; }

        public string CustomerId { get; set; } = string.Empty;

        public IReadOnlyList<OrderItemRequest> OrderItems { get; set; } = new List<OrderItemRequest>();
    }
}
