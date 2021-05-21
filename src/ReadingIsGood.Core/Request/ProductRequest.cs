using ReadingIsGood.Core.CQRS;

namespace ReadingIsGood.Core.Request
{
    public class ProductRequest : ICommand<string>
    {
        public string SKU { get; set; } = string.Empty;

        public int Stock;
    }
}
