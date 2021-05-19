using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReadingIsGood.Core.Entities
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string OrderNumber { get; set; } = string.Empty;
    }
}
