using ReadingIsGood.Application.Mediator.Query;
using ReadingIsGood.Core.Query;
using ReadingIsGood.Core.Response;
using ReadingIsGood.Core.Services.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace ReadingIsGood.Application.Handlers.Product
{
    public class GetProductQueryHandler : IQueryHandler<GetProductQuery, ProductResponse>
    {
        private readonly IProductService productService;
        public GetProductQueryHandler(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<ProductResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            return await productService.GetProductAsync(request);
        }
    }
}
