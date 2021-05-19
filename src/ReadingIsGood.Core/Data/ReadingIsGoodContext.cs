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
            Product = database.GetCollection<Product>("Product");
            //CustomerContextSeed.SeedData(Customer);
            //ProductContextSeed.SeedData(Product);

#pragma warning disable CS0618 // Type or member is obsolete
            Customer.Indexes.CreateOne(
                Builders<Customer>.IndexKeys.Ascending(x => x.Name),
                new CreateIndexOptions { Unique = true });

            Product.Indexes.CreateOne(
                Builders<Product>.IndexKeys.Ascending(x => x.SKU),
                new CreateIndexOptions { Unique = true });
#pragma warning restore CS0618 // Type or member is obsolete
        }

        public IMongoCollection<Customer> Customer { get; }

        public IMongoCollection<Order> Order { get; }

        public IMongoCollection<Product> Product { get; }
    }
}
