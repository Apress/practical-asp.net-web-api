using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http.Filters;

namespace WebCaching
{
    public class CacheAttribute : ActionFilterAttribute
    {
        public double MaxAgeSeconds { get; set; }

        public override void OnActionExecuted(HttpActionExecutedContext context)
        {
            if (this.MaxAgeSeconds > 0)
            {
                context.Response.Headers.CacheControl = new CacheControlHeaderValue()
                {
                    MaxAge = TimeSpan.FromSeconds(this.MaxAgeSeconds),
                    MustRevalidate = true,
                    Private = true
                };
            }
        }
    }
}