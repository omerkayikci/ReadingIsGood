using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReadingIsGood.Core.Entities
{
    public class Order
    {
        public Order(
            string orderNumber)
        {
            this.OrderNumber = orderNumber;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string OrderNumber { get; private set; }
    }
}
