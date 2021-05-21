using ReadingIsGood.Core.CQRS;
using ReadingIsGood.Core.Response;

namespace ReadingIsGood.Core.Query
{
    public class GetProductQuery : IQuery<ProductResponse>
    {
        public string productId { get; set; } = string.Empty;
    }
}
