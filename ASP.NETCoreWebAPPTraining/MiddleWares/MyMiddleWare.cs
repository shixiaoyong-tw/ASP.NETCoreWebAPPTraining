using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ASP.NETCoreWebAPPTraining.MiddleWares
{
    public class MyMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public MyMiddleWare(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<MyMiddleWare>();
        }

        public async Task Invoke(HttpContext context)
        {
            var originalBody = context.Response.Body;
            await using var newBody = new MemoryStream();
            context.Response.Body = newBody;

            try
            {
                await _next(context);
            }
            finally
            {
                newBody.Seek(0, SeekOrigin.Begin);
                var bodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
                var logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]," +
                                 $"Request TraceID:{context.TraceIdentifier}," +
                                 $"response body:{bodyText}";
                _logger.LogInformation(logMessage);
                newBody.Seek(0, SeekOrigin.Begin);
                await newBody.CopyToAsync(originalBody);
            }
        }
    }
}