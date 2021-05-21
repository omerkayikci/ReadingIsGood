using System;

namespace ReadingIsGood.Core.Entities
{
    public class OrderItem
    {
        public Guid TraceId { get; set; } = Guid.NewGuid();

        public string ProductId { get; set; } = string.Empty;

        public string ProductSku { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public decimal? TotalPrice { get; set; }
    }
}
