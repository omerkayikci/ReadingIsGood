using System;

namespace ReadingIsGood.Core.Response
{
    public class OrderItemResponse
    {
        public Guid TraceId { get; set; } = Guid.NewGuid();

        public string ProductId { get; set; } = string.Empty;

        public string ProductSku { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public decimal? TotalPrice { get; set; }
    }
}
