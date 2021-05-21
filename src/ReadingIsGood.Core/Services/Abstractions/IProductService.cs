using ReadingIsGood.Core.Query;
using ReadingIsGood.Core.Request;
using ReadingIsGood.Core.Response;
using System.Threading.Tasks;

namespace ReadingIsGood.Core.Services.Abstractions
{
    public interface IProductService : IApplicationService
    {
        Task<ProductResponse> GetProductAsync(GetProductQuery query);
        Task<string> AddProductAsync(ProductRequest request);
        Task<string> UpdateProductStockAsync(UpdateStockRequest request);
    }
}
