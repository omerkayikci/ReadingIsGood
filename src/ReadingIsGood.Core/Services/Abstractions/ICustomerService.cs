﻿using ReadingIsGood.Core.Query;
using ReadingIsGood.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReadingIsGood.Core.Services.Abstractions
{
    public interface ICustomerService : IApplicationService
    {
        Task<CustomerResponse> GetCustomerAsync(GetCustomerQuery request);
    }
}