using ReadingIsGood.Core.CQRS;
using ReadingIsGood.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReadingIsGood.Core.Query
{
    public class GetProductQuery : IQuery<ProductResponse>
    {
        public string productId { get; set; } = string.Empty;
    }
}
