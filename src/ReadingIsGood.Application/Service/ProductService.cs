using Microsoft.Extensions.Logging;
using ReadingIsGood.Application.Extensions;
using ReadingIsGood.Common.ExceptionHandling;
using ReadingIsGood.Core.Entities;
using ReadingIsGood.Core.Query;
using ReadingIsGood.Core.Repositories.Abstractions;
using ReadingIsGood.Core.Request;
using ReadingIsGood.Core.Response;
using ReadingIsGood.Core.Services.Abstractions;
using System.Net;
using System.Threading.Tasks;

namespace ReadingIsGood.Application.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<string> AddProductAsync(ProductRequest request)
        {
            Product product = new Product
            {
                SKU = request.SKU,
                Stock = request.Stock
            };

            await this.productRepository.AddProductAsync(product);

            return product.Id;
        }

        public async Task<ProductResponse> GetProductAsync(GetProductQuery query)
        {
            Product? product = await this.productRepository.GetProductAsync(query.productId);

            if (product == null)
            {
                throw new ReadingIsGoodException($"The customer not found by product id. Id: {query.productId}", HttpStatusCode.NotFound, logLevel: LogLevel.Warning);
            }

            return product.ToProductResponse();
        }

        public async Task<string> UpdateProductStockAsync(UpdateStockRequest request)
        {
            string? id = await this.productRepository.UpdateProductStockAsync(request.Id, request.Stock, request.UpdatedDateTime);

            if (string.IsNullOrEmpty(id))
            {
                throw new ReadingIsGoodException($"The customer not updated stock by product id. Id: {request.Id}", HttpStatusCode.InternalServerError, logLevel: LogLevel.Warning);
            }

            return id;
        }
    }
}
