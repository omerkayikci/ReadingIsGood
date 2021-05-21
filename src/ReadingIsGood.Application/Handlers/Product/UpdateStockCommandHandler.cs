using ReadingIsGood.Application.Mediator.Command;
using ReadingIsGood.Core.Request;
using ReadingIsGood.Core.Services.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace ReadingIsGood.Application.Handlers.Product
{
    public class UpdateStockCommandHandler : ICommandHandler<UpdateStockRequest, string>
    {
        private readonly IProductService productService;
        public UpdateStockCommandHandler(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<string> Handle(UpdateStockRequest request, CancellationToken cancellationToken)
        {
            return await this.productService.UpdateProductStockAsync(request);
        }
    }
}
