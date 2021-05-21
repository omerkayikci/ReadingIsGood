using ReadingIsGood.Core.Entities;
using ReadingIsGood.Core.Request;
using ReadingIsGood.Core.Response;

namespace ReadingIsGood.Application.Extensions
{
    public static class ProductMapperExtensions
    {
        public static ProductResponse ToProductResponse(this Product response)
        {
            return new ProductResponse
            {
                SKU = response.SKU,
                CreatedDateTime = response.CreatedDateTime,
                Stock = response.Stock,
                UpdatedDateTime = response.UpdatedDateTime
            };
        }

        public static Product ToProduct(this ProductRequest request)
        {
            return new Product
            {
                SKU = request.SKU,
                Stock = request.Stock
            };
        }
    }
}
