using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using ReadingIsGood.Common.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace ReadingIsGood.Api.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandlerMiddleware> logger;
        private readonly Func<object, Task> clearCacheHeadersDelegate;

        public ExceptionHandlerMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlerMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
            this.clearCacheHeadersDelegate = ClearCacheHeadersAsync;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "ErrorHandlingMiddleware should catch all errors!")]
        public async Task InvokeAsync(HttpContext context)
        {
            ExceptionDispatchInfo edi;

            try
            {
                Task task = this.next(context);

                if (!task.IsCompletedSuccessfully)
                {
                    await Awaited(this, context, task);
                }

                return;
            }
            catch (Exception exception)
            {
                // Get the Exception, but don't continue processing in the catch block as its bad for stack usage.
                edi = ExceptionDispatchInfo.Capture(exception);
            }

            await this.HandleExceptionAsync(context, edi);

            static async Task Awaited(ExceptionHandlerMiddleware middleware, HttpContext context, Task task)
            {
                ExceptionDispatchInfo? edi = null;

                try
                {
                    await task;
                }
                catch (Exception exception)
                {
                    // Get the Exception, but don't continue processing in the catch block as its bad for stack usage.
                    edi = ExceptionDispatchInfo.Capture(exception);
                }

                if (edi == null)
                {
                    return;
                }

                await middleware.HandleExceptionAsync(context, edi);
            }
        }

        private static void ClearHttpContext(HttpContext context)
        {
            context.Response.Clear();

            // An endpoint may have already been set. Since we're going to re-invoke the middleware pipeline we need to reset
            // the endpoint and route values to ensure things are re-calculated.
            context.SetEndpoint(null);
            IRouteValuesFeature routeValuesFeature = context.Features.Get<IRouteValuesFeature>();
            routeValuesFeature?.RouteValues?.Clear();
        }

        private static Task ClearCacheHeadersAsync(object state)
        {
            IHeaderDictionary headers = ((HttpResponse)state).Headers;
            headers[HeaderNames.CacheControl] = "no-cache";
            headers[HeaderNames.Pragma] = "no-cache";
            headers[HeaderNames.Expires] = "-1";
            headers.Remove(HeaderNames.ETag);
            return Task.CompletedTask;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "ErrorHandlingMiddleware should catch all errors!")]
        private async Task HandleExceptionAsync(HttpContext context, ExceptionDispatchInfo edi)
        {
            PathString originalPath = context.Request.Path;

            // We can't do anything if the response has already started, just abort.
            if (context.Response.HasStarted)
            {
                this.logger.LogError(edi.SourceException, $"An unhandled exception was thrown by the application. Path:{originalPath}, TraceId:{context.TraceIdentifier}");

                this.logger.LogWarning(edi.SourceException, "The response has already started, the error handler will not be executed.");

                var error = new Dictionary<string, object?>
                {
                    ["data"] = null,
                    ["message"] = edi.SourceException.Message,
                };

                await context.Response.WriteAsync(JsonConvert.SerializeObject(error));
                return;
            }

            try
            {
                if (edi.SourceException is ReadingIsGoodException readingIsGoodException)
                {
                    LogLevel logLevel = readingIsGoodException.LogLevel ?? LogLevel.Error;

                    this.logger.Log(logLevel, readingIsGoodException, "An SLMPException was thrown by the application: " + readingIsGoodException.Message);
                }
                else
                {
                    this.logger.Log(LogLevel.Error, edi.SourceException, $"An unhandled exception was thrown by the application: {edi.SourceException.Message}");
                    readingIsGoodException = new ReadingIsGoodException($"An unhandled exception was thrown by the application: {edi.SourceException.Message}", HttpStatusCode.InternalServerError, edi.SourceException);
                }

                if (context.Response.HasStarted)
                {
                    ClearHttpContext(context);
                }

                int httpStatus = readingIsGoodException.HttpStatusCode != null ? (int)readingIsGoodException.HttpStatusCode : (!context.Response.HasStarted && context.Response.StatusCode == 200 ? 500 : context.Response.StatusCode);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = httpStatus;
                var exceptionHandlerFeature = new ExceptionHandlerFeature()
                {
                    Error = edi.SourceException,
                    Path = originalPath.Value,
                };
                context.Features.Set<IExceptionHandlerFeature>(exceptionHandlerFeature);
                context.Features.Set<IExceptionHandlerPathFeature>(exceptionHandlerFeature);
                context.Response.OnStarting(this.clearCacheHeadersDelegate, context.Response);

                //context.Request.Headers.TryGetValue(CommandApiConstants.HeaderCorreleationIdKey, out StringValues traceIds);

                var error = new Dictionary<string, object?>
                {
                    ["data"] = readingIsGoodException.ErrorData,
                    ["message"] = readingIsGoodException.Message,
                };

                await context.Response.WriteAsync(JsonConvert.SerializeObject(error));

                return;
            }
            catch (Exception ex2)
            {
                // Suppress secondary exceptions, re-throw the original.
                this.logger.LogError(ex2, "An exception was thrown attempting to execute the error handler.");
            }
            finally
            {
                context.Request.Path = originalPath;
            }

            edi.Throw(); // Re-throw the original if we couldn't handle it
        }
    }
}
