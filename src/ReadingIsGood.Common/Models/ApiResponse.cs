﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ReadingIsGood.Common.Models
{
    public class ApiResponse<T> where T : class
    {
        public T? Data { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
