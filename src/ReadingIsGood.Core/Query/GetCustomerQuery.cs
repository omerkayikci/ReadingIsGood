﻿using ReadingIsGood.Core.CQRS;
using ReadingIsGood.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReadingIsGood.Core.Query
{
    public class GetCustomerQuery : IQuery<CustomerResponse>
    {
        public string? customerId { get; set; }
    }
}
