using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ReadingIsGood.Core.Data.Abstractions;
using ReadingIsGood.Core.Entities;
using ReadingIsGood.Core.Options;

namespace ReadingIsGood.Core.Data
{
    public class ReadingIsGoodContext : IReadingIsGoodContext
    {
        public ReadingIsGoodContext(
            IOptions<DatabaseSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            var database = client.GetDatabase(options.Value.DatabaseName);

            Customer = database.GetCollection<Customer>("Customers");
            Order = database.GetCollection<Order>("Orders");
            CustomerContextSeed.SeedData(Customer);
        }

        public IMongoCollection<Customer> Customer { get; }

        public IMongoCollection<Order> Order { get; }
    }
}
