using ReadingIsGood.Common.Enums;
using ReadingIsGood.MongoDB.Abstractions;
using System.Collections.Generic;

namespace ReadingIsGood.Core.Entities
{
    public class Order : IEntity<string>
    {
        public string Id { get; set; } = string.Empty;

        public string? OrderNote { get; set; }

        public string CustomerId { get; set; } = string.Empty;

        public OrderStatus OrderStatus { get; set; }

        public int OrderCount { get; set; }

        public IReadOnlyList<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
