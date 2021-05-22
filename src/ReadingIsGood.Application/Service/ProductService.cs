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
        private ILogger<ProductService> logger;
        public ProductService(IProductRepository productRepository,
            ILogger<ProductService> logger)
        {
            this.productRepository = productRepository;
            this.logger = logger;
        }

        public async Task<string> AddProductAsync(ProductRequest request)
        {
            Product product = request.ToProduct();

            await this.productRepository.AddProductAsync(product);

            return product.Id;
        }

        public async Task<ProductResponse> GetProductAsync(GetProductQuery query)
        {
            Product? product = await this.productRepository.GetProductByIdAsync(query.productId);

            if (product == null)
            {
                this.logger.LogWarning($"The product not found by product id. Id: {query.productId}");
                throw new ReadingIsGoodException("The product not found by product id", HttpStatusCode.NotFound, logLevel: LogLevel.Warning);
            }

            return product.ToProductResponse();
        }

        public async Task<string> UpdateProductStockAsync(UpdateStockRequest request)
        {
            string? id = await this.productRepository.UpdateProductStockAsync(request.Id, request.Stock, request.UpdatedDateTime, request.IsDecrease);

            if (string.IsNullOrEmpty(id))
            {
                this.logger.LogWarning($"The product not updated stock by product id. Id: {request.Id}");
                throw new ReadingIsGoodException("The product not updated stock by product id.", HttpStatusCode.InternalServerError, logLevel: LogLevel.Warning);
            }

            return id;
        }
    }
}
