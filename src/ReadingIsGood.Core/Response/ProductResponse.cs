using System;

namespace ReadingIsGood.Core.Response
{
    public class ProductResponse
    {
        public string? SKU { get; set; }
        public int? Stock { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? UpdatedDateTime { get; set; }

    }
}
