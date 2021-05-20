using ReadingIsGood.MongoDB.Abstractions;
using System;

namespace ReadingIsGood.Core.Entities
{
    public class Product : IEntity<string>
    {
        public string Id { get; set; }

        public string SKU { get; set; } = string.Empty;

        public int Stock { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDateTime { get; set; }
    }
}
