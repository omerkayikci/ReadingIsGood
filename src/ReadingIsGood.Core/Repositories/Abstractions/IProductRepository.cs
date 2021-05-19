using ReadingIsGood.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReadingIsGood.Core.Repositories.Abstractions
{
    public interface IProductRepository
    {
        Task<Product?> GetProductAsync(string id);
        Task AddProductAsync(Product product);
        Task<string> UpdateProductStockAsync(string id, int stock, DateTime updatedDateTime);
    }
}
