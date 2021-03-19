using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ASP.NETCoreWebAPPTraining.Filters
{
    public class MyAuthorizationFilterAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var isHave = context.HttpContext.Request.Headers.TryGetValue("Authorization", out var value);

            if (!isHave)
            {
                context.HttpContext.Response.StatusCode = 403;
                context.Result = new JsonResult("Authorization Fail");
            }
        }
    }
}