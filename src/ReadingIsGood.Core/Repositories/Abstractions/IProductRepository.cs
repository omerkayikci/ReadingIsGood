using ReadingIsGood.Core.Entities;
using System;
using System.Threading.Tasks;

namespace ReadingIsGood.Core.Repositories.Abstractions
{
    public interface IProductRepository
    {
        Task<Product?> GetProductByIdAsync(string id);
        Task AddProductAsync(Product product);
        Task<string> UpdateProductStockAsync(string id, int stock, DateTime updatedDateTime, bool decrease);
        Task<bool> CheckProductStockAvailablityAsync(Product product, int orderQuantity);
        Task<bool> StockIsAvailableAsync(Product product, int quantity);
    }
}
