using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Tracing;

namespace HelloWebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //config.EnableSystemDiagnosticsTracing();
            config.Services.Replace(typeof(ITraceWriter), new WebApiTracer());

            //config.Services.Replace(typeof(System.Web.Http.Tracing.ITraceWriter), new EntryExitTracer());

            config.MessageHandlers.Add(new TracingHandler());
        }
    }
}
