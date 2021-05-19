using ReadingIsGood.Core.CQRS;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReadingIsGood.Core.Request
{
    public class CustomerRequest : ICommand<string>
    {
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
