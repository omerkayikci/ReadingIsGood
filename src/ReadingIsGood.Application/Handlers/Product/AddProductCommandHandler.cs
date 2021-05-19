using ReadingIsGood.Application.Mediator.Command;
using ReadingIsGood.Core.Request;
using ReadingIsGood.Core.Services.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace ReadingIsGood.Application.Handlers.Product
{
    public class AddProductCommandHandler : ICommandHandler<ProductRequest, string>
    {
        private readonly IProductService productService;
        public AddProductCommandHandler(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<string> Handle(ProductRequest request, CancellationToken cancellationToken)
        {
            return await this.productService.AddProductAsync(request);
        }
    }
}
