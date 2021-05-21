using ReadingIsGood.Common.Enums;

namespace ReadingIsGood.Core.Response
{
    public class AllOrderResponse
    {
        public string OrderId { get; set; } = string.Empty;

        public OrderStatus OrderStatus { get; set; }
    }
}
