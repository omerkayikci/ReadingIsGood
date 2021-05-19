using ReadingIsGood.Core.CQRS;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReadingIsGood.Core.Request
{
    public class UpdateStockRequest : ICommand<string>
    {
        public string Id { get; set; } = string.Empty;
        public int Stock { get; set; }
        public DateTime UpdatedDateTime { get; set; } = DateTime.UtcNow;
    }
}
