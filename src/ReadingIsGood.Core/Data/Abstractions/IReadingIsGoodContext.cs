using MongoDB.Driver;
using ReadingIsGood.Core.Entities;

namespace ReadingIsGood.Core.Data.Abstractions
{
    public interface IReadingIsGoodContext
    {
        IMongoCollection<Customer> Customer { get; }

        IMongoCollection<Order> Order { get; }
    }
}
