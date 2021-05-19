﻿using ReadingIsGood.Core.Entities;
using ReadingIsGood.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReadingIsGood.Application.Extensions
{
    public static class ProductMapperExtensions
    {
        public static ProductResponse ToProductResponse(this Product response)
        {
            return new ProductResponse
            {
                SKU = response.SKU,
                CreatedDateTime = response.CreatedDateTime,
                Stock = response.Stock,
                UpdatedDateTime = response.UpdatedDateTime
            };
        }
    }
}
