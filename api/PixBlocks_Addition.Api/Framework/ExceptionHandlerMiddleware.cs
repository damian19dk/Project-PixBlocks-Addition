using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PixBlocks_Addition.Domain.Exceptions;
using PixBlocks_Addition.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Api.Framework
{
    public class ExceptionHandlerMiddleware
    {
        public readonly RequestDelegate _next;
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _configuration;

        public ExceptionHandlerMiddleware(RequestDelegate next, IMemoryCache cache, IConfiguration configuration)
        {
            _next = next;
            _cache = cache;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _cache, _configuration);
            }
        }

        public static Task HandleExceptionAsync(HttpContext context, Exception exception, IMemoryCache cache, IConfiguration configuration)
        {
            var errorCode = "error";
            var statusCode = HttpStatusCode.BadRequest;
            var exceptionType = exception.GetType();
            var exceptionMessage = exception.Message;
            var language = context.Request.Headers["Accept-Language"].ToString().ToLower();
            if (language != "pl" && language != "en")
                language = "en";
            switch (exception)
            {
                case Exception e when exceptionType == typeof(UnauthorizedAccessException):
                    statusCode = HttpStatusCode.Unauthorized;
                    break;
                case MyException e when exceptionType == typeof(MyException):
                    statusCode = HttpStatusCode.BadRequest;
                    var key = e.Message + "-" + language;
                    if (!cache.TryGetValue(key, out exceptionMessage))
                    {
                        ExceptionMessages.ExceptionMessages.LoadMessagesToCache(cache, configuration);
                        exceptionMessage = cache.Get<string>(key);
                    }
                    errorCode = e.Code;
                    break;
                case Exception e when exceptionType == typeof(Exception):
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            var response = new { code = errorCode, message = exceptionMessage };
            var payload = JsonConvert.SerializeObject(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(payload);
        }
    }
}

