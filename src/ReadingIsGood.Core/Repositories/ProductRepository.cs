using Microsoft.Extensions.Logging;
using ReadingIsGood.Common.ExceptionHandling;
using ReadingIsGood.Core.Entities;
using ReadingIsGood.Core.Repositories.Abstractions;
using ReadingIsGood.MongoDB.Abstractions;
using System;
using System.Threading.Tasks;

namespace ReadingIsGood.Core.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IGenericRepository<Product, string> genericRepository;

        public ProductRepository(
            IGenericRepository<Product, string> genericRepository)
        {
            this.genericRepository = genericRepository;
        }

        public async Task AddProductAsync(Product product)
        {
            await this.genericRepository
                              .AddOneAsync(product);
        }

        public async Task<Product?> GetProductByIdAsync(string id)
        {
            return await this.genericRepository
                                .GetByIdAsync(id);
        }

        //stock reconciliation olarak düşünülebilir ve stock değerinin sıfırlanmasıda yapılabilir.
        public async Task<string> UpdateProductStockAsync(string id, int stock, DateTime updatedDateTime, bool decrease)
        {
            var product = await this.genericRepository.GetByIdAsync(id);

            if (product == null)
            {
                throw new ReadingIsGoodException($"Product not found.ProductId: {id}", System.Net.HttpStatusCode.NotFound, logLevel: LogLevel.Warning);
            }

            if (decrease)
            {
                product!.Stock -= stock;
            }
            else
            {
                product!.Stock += stock;
            }

            await this.genericRepository.UpdateAsync(product);

            return id;
        }

        public async Task<bool> CheckProductStockAvailablityAsync(Product product, int orderQuantity)
        {
            var stockExist = await StockIsAvailableAsync(product, orderQuantity);
            if (!stockExist)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> StockIsAvailableAsync(Product product, int quantity)
        {
            return product.Stock >= quantity;
        }
    }
}
