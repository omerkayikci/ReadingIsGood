namespace ReadingIsGood.Core.Request
{
    public class OrderItemRequest
    {
        public string ProductId { get; set; } = string.Empty;

        public string ProductSku { get; set; } = string.Empty;

        public int? Quantity { get; set; }

        public decimal? TotalPrice { get; set; }
    }
}
