using ReadingIsGood.Core.CQRS;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReadingIsGood.Core.Request
{
    public class ProductRequest : ICommand<string>
    {
        public string SKU { get; set; } = string.Empty;

        public int Stock;
    }
}
