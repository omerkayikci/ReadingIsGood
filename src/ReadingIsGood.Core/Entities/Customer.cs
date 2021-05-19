using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ReadingIsGood.Core.Entities
{
    public class Customer
    {
        public Customer(
            Guid customerId,
            string name,
            string phoneNumber,
            string email,
            string address)
        {
            this.CustomerId = customerId;
            this.Name = name;
            this.PhoneNumber = phoneNumber;
            this.Email = email;
            this.Address = address;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Guid CustomerId { get; set; }
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public string Address { get; private set; }
    }
}
