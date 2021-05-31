using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ReadingIsGood.MongoDB.Abstractions;
using System.Threading.Tasks;

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

        public async Task<ITransactionScope> BeginTransactionScopeAsync()
        {
            IClientSessionHandle session = await this.Client.StartSessionAsync();
            return new MongoDBTransactionScope(this, session);
        }

        public ITransactionScope BeginTransactionScope()
        {
            IClientSessionHandle session = this.Client.StartSession();
            return new MongoDBTransactionScope(this, session);
        }
    }

}
