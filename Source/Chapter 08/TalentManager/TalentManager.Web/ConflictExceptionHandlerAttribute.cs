using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace TalentManager.Web
{
    public class ConflictExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            string message = "Changes not saved because of missing or stale ETag. ";
            message += "GET the resource and retry with the new ETag";

            if (context.Exception is DbUpdateConcurrencyException)
            {
                context.Response = context.Request.CreateErrorResponse(
                                        HttpStatusCode.Conflict,
                                            message);
            }
        }
    }
}