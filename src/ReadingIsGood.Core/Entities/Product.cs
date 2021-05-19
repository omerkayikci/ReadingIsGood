using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ReadingIsGood.Core.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string SKU { get; set; } = string.Empty;

        public int Stock { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDateTime { get; set; }
    }
}
