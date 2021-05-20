using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ReadingIsGood.MongoDB.Abstractions;

namespace ReadingIsGood.MongoDB
{
    public class MongoDBDatabaseConnection : IDatabaseConnection
    {
        public MongoDBDatabaseConnection(IOptions<MongoDbOptions> options)
        {
            this.Client = new MongoClient(options.Value.ConnectionString);
            this.Database = this.Client.GetDatabase(options.Value.Database);
        }

        internal MongoClient Client { get; }

        internal IMongoDatabase Database { get; }
    }

}
