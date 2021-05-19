using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReadingIsGood.Core.Data;
using ReadingIsGood.Core.Data.Abstractions;
using ReadingIsGood.Core.Options;
using ReadingIsGood.Core.Repositories;
using ReadingIsGood.Core.Repositories.Abstractions;

namespace ReadingIsGood.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMongoDB();

            services.Configure<DatabaseSettings>(configuration.GetSection("ReadingIsGoodDatabaseSettings"));

            return services;
        }

        public static IServiceCollection AddMongoDB(this IServiceCollection services)
        {
            services.AddSingleton<IReadingIsGoodContext, ReadingIsGoodContext>();
            services.AddSingleton<ICustomerRepository, CustomerRepository>();
            services.AddSingleton<IOrderRespository, OrderRepository>();
            services.AddSingleton<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
