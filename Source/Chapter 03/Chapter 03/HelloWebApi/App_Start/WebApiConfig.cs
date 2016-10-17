using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using HelloWebApi.Models;

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

            //config.Formatters.Add(
            //        new FixedWidthTextMediaFormatter());

            var fwtMediaFormatter = new FixedWidthTextMediaFormatter();

            fwtMediaFormatter.MediaTypeMappings.Add(
                new QueryStringMapping("frmt", "fwt",
                    new MediaTypeHeaderValue("text/plain")));

            config.Formatters.Add(fwtMediaFormatter);

            config.Formatters.Add(new JsonpMediaTypeFormatter());

            //config.Formatters.RemoveAt(0);
            //config.Formatters.RemoveAt(0);

            //config.Formatters.JsonFormatter
            //              .MediaTypeMappings.Add(
            //                       new QueryStringMapping("frmt", "json",
            //                                                     new MediaTypeHeaderValue("application/json")));
            //config.Formatters.XmlFormatter
            //                     .MediaTypeMappings.Add(
            //                               new QueryStringMapping("frmt", "xml",
            //                                                           new MediaTypeHeaderValue("application/xml")));

            //config.Formatters.JsonFormatter
            //                    .MediaTypeMappings.Add(new RequestHeaderMapping(
            //                                         "X-Media", "json",
            //                                                StringComparison.OrdinalIgnoreCase, false,
            //                                                      new MediaTypeHeaderValue("application/json")));

            //config.Formatters.JsonFormatter
            //                    .MediaTypeMappings.Add(new IPBasedMediaTypeMapping());


            //foreach (var formatter in config.Formatters)
            //{
            //    Trace.WriteLine(formatter.GetType().Name);
            //    Trace.WriteLine("\tCanReadType: " + formatter.CanReadType(typeof(Employee)));
            //    Trace.WriteLine("\tCanWriteType: " + formatter.CanWriteType(typeof(Employee)));
            //    Trace.WriteLine("\tBase: " + formatter.GetType().BaseType.Name);
            //    Trace.WriteLine("\tMedia Types: " + String.Join(", ", formatter.SupportedMediaTypes));
            //}

        }
    }
}
