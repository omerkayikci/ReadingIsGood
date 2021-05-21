using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Pluralize.NET;
using ReadingIsGood.MongoDB.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingIsGood.MongoDB
{
    public sealed class MongoDbRepository<TEntity, TId> : IGenericRepository<TEntity, TId>
         where TEntity : class, IEntity<TId>
         where TId : IEquatable<TId>
    {
        private static readonly Pluralizer Pluralizer = new Pluralizer();
        private readonly MongoDBDatabaseConnection databaseConnection;
        private readonly Lazy<IMongoCollection<TEntity>> lazyCollection;
        private string customCollectionName;

        public MongoDbRepository(IDatabaseConnection databaseConnection)
        {
            if (!(databaseConnection is MongoDBDatabaseConnection mongoDBDatabaseConnection))
            {
                throw new InvalidOperationException("IDatabaseConnection is not MongoDBDatabaseConnection!");
            }

            this.databaseConnection = mongoDBDatabaseConnection;
            this.lazyCollection = new Lazy<IMongoCollection<TEntity>>(() => this.CreateCollectionFactory());
        }

        public string CollectionName
        {
            get
            {
                return this.customCollectionName;
            }
            set
            {
                if (this.lazyCollection.IsValueCreated)
                {
                    throw new InvalidOperationException("Cannot set the CustomCollectionName after the collections is created!");
                }
            }
        }

        public bool CollectionExists
        {
            get
            {

                var filter = new BsonDocument("name", this.GetCollectionName());
                var options = new ListCollectionNamesOptions { Filter = filter };

                return this.databaseConnection.Database.ListCollectionNames(options).Any();
            }
        }

        public static string DefaultCollectionName
        {
            get
            {
                string plural = Pluralizer.Pluralize(typeof(TEntity).Name);
                return plural.Substring(0, 1).ToLowerInvariant() + plural.Substring(1);
            }
        }

        public bool CreateCollection()
        {
            if (this.CollectionExists)
            {
                return false;
            }

            this.databaseConnection.Database.CreateCollection(this.Collection.CollectionNamespace.CollectionName);
            return true;
        }

        public bool DropCollection()
        {
            if (!this.CollectionExists)
            {
                return false;
            }

            this.databaseConnection.Database.DropCollection(this.Collection.CollectionNamespace.CollectionName);
            return true;
        }

        public async Task<TEntity?> GetByIdAsync(TId id)
        {
            return await this.Collection.AsQueryable().Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public void AddMany(IEnumerable<TEntity> entity)
        {
            this.Collection.InsertMany(entity);
        }

        public async Task AddManyAsync(IEnumerable<TEntity> entity)
        {
            await this.Collection.InsertManyAsync(entity);
        }

        public void AddOne(TEntity entity)
        {
            this.Collection.InsertOne(entity);
        }

        public async Task AddOneAsync(TEntity entity)
        {
            await this.Collection.InsertOneAsync(entity);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await this.Collection.DeleteOneAsync(e => e.Id.Equals(entity.Id));
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await this.Collection.ReplaceOneAsync(e => e.Id.Equals(entity.Id), entity);
        }

        private IMongoCollection<TEntity> CreateCollectionFactory()
        {
            return this.databaseConnection.Database.GetCollection<TEntity>(this.GetCollectionName());
        }

        private string GetCollectionName()
        {
            return this.CollectionName ?? DefaultCollectionName;
        }

        private IMongoCollection<TEntity> Collection
        {
            get { return this.lazyCollection.Value; }
        }

        public IGenericRepositoryQueryBuilder<TEntity> Query()
        {
            return new MongoDbRepositoryQueryBuilder<TEntity>(this.Collection.AsQueryable());
        }
    }
}
