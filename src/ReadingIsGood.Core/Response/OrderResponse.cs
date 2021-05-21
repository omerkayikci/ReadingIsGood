using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ReadingIsGood.Common.Enums;
using System.Collections.Generic;


namespace ReadingIsGood.Core.Response
{
    public class OrderResponse
    {
        public string? OrderNote { get; set; }

        public string CustomerId { get; set; } = string.Empty;

        [JsonConverter(typeof(StringEnumConverter))]
        public OrderStatus OrderStatus { get; set; }

        public int OrderCount { get; set; }

        public IReadOnlyList<OrderItemResponse> OrderItems { get; set; } = new List<OrderItemResponse>();
    }
}
