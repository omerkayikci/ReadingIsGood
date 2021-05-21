using ReadingIsGood.Common.Enums;
using ReadingIsGood.Core.CQRS;

namespace ReadingIsGood.Core.Request
{
    public class OrderStatusUpdateRequest : ICommand<string>
    {
        public string OrderId { get; set; } = string.Empty;

        public string CustomerId { get; set; } = string.Empty;

        public string OrderStatus { get; set; } = string.Empty;
    }
}
