using Microsoft.Extensions.Logging;
using System;
using System.Net;

namespace ReadingIsGood.Common.ExceptionHandling
{
    public class ReadingIsGoodException : Exception
    {
        public ReadingIsGoodException(string message, HttpStatusCode? httpStatusCode = null, Exception? innerException = null, LogLevel logLevel = Microsoft.Extensions.Logging.LogLevel.Trace, object? errorData = null)
            : base(message, innerException)
        {
            this.Message = message;
            this.HttpStatusCode = httpStatusCode;
            this.LogLevel = logLevel;
            this.ErrorData = errorData;
        }
        public string? Message { get; }

        public HttpStatusCode? HttpStatusCode { get; }

        public LogLevel? LogLevel { get; }

        public object? ErrorData { get; }
    }
}
