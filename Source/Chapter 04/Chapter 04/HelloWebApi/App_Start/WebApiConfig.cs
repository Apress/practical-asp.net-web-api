using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;

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

            config.Formatters.JsonFormatter
                    .SerializerSettings
                        .Converters.Add(new NumberConverter());

            config.Formatters.JsonFormatter
                    .SerializerSettings
                        .Converters.Add(new DateTimeConverter());

            //config.Formatters.JsonFormatter
            //                     .SupportedEncodings
            //                          .Add(Encoding.GetEncoding(932));

            //foreach (var encoding in config.Formatters.JsonFormatter.SupportedEncodings)
            //{
            //    System.Diagnostics.Trace.WriteLine(encoding.WebName);
            //}

            //config.MessageHandlers.Add(new EncodingHandler());

            config.MessageHandlers.Add(new CultureHandler());

        }
    }
}
