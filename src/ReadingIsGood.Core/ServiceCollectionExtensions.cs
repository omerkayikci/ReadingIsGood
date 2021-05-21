using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using ReadingIsGood.Core.Entities;
using ReadingIsGood.Core.Repositories;
using ReadingIsGood.Core.Repositories.Abstractions;
using ReadingIsGood.Core.Services;
using ReadingIsGood.Core.Services.Abstractions;
using ReadingIsGood.MongoDB;
using ReadingIsGood.MongoDB.Abstractions;

namespace ReadingIsGood.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMongoOptionDriver()
                    .AddMongoDB();

            services.AddSingleton<ISeedService, SeedService>();

            services.Configure<MongoDbOptions>(configuration.GetSection("MongoDbOptions"));

            return services;
        }

        public static IServiceCollection AddMongoOptionDriver(this IServiceCollection services)
        {
            BsonClassMap.RegisterClassMap<Customer>(pm =>
           {
               pm.AutoMap();
               pm.MapIdProperty(p => p.Id)
               .SetSerializer(new StringSerializer(BsonType.ObjectId))
               .SetIdGenerator(StringObjectIdGenerator.Instance);
               pm.SetIgnoreExtraElements(true);
           });

            BsonClassMap.RegisterClassMap<Product>(pm =>
            {
                pm.AutoMap();
                pm.MapIdProperty(p => p.Id)
                .SetSerializer(new StringSerializer(BsonType.ObjectId))
                .SetIdGenerator(StringObjectIdGenerator.Instance);
                pm.SetIgnoreExtraElements(true);
            });

            BsonClassMap.RegisterClassMap<User>(pm =>
            {
                pm.AutoMap();
                pm.MapIdProperty(p => p.Id)
                .SetSerializer(new StringSerializer(BsonType.ObjectId))
                .SetIdGenerator(StringObjectIdGenerator.Instance);
                pm.SetIgnoreExtraElements(true);
            });

            BsonClassMap.RegisterClassMap<Order>(pm =>
            {
                pm.AutoMap();
                pm.MapIdProperty(p => p.Id)
                .SetSerializer(new StringSerializer(BsonType.ObjectId))
                .SetIdGenerator(StringObjectIdGenerator.Instance);
                pm.SetIgnoreExtraElements(true);
            });

            return services;
        }


        public static IServiceCollection AddMongoDB(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IDatabaseConnection), typeof(MongoDBDatabaseConnection));

            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<ICustomerRepository, CustomerRepository>();
            services.AddSingleton<IOrderRespository, OrderRepository>();
            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddSingleton(typeof(IGenericRepository<,>), typeof(MongoDbRepository<,>));

            return services;
        }
    }
}
