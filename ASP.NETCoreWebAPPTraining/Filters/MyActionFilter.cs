using System;
using System.IO;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace ASP.NETCoreWebAPPTraining.Filters
{
    public class MyActionFilter : IActionFilter
    {
        private readonly ILogger _logger;

        public MyActionFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<MyActionFilter>();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var contextInfo = context.HttpContext;
            var requestInfo = contextInfo.Request;

            var logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]:[{requestInfo.Method}]," +
                             $"Request TraceID:{contextInfo.TraceIdentifier}";

            if (requestInfo.Method == "POST" || requestInfo.Method == "PUT")
            {
                // get request body value
                requestInfo.Body.Position = 0;
                var streamReader = new StreamReader(requestInfo.Body);
                var requestBodyValue = streamReader.ReadToEnd();
                requestInfo.Body.Position = 0;

                logMessage += $",request body:{requestBodyValue}";
            }

            _logger.LogInformation(logMessage);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }
}