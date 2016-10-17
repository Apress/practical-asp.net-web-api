using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using RequestBinding.Models;

namespace RequestBinding
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

            config.Formatters.Add(
                    new FixedWidthTextMediaFormatter());

            //config.Formatters.JsonFormatter
            //    .SerializerSettings.Culture = new System.Globalization.CultureInfo("en-GB");

            config.Services.Add(typeof(System.Web.Http.ValueProviders.ValueProviderFactory),
                                                   new HeaderValueProviderFactory());
            
            config.Formatters.JsonFormatter
                .SerializerSettings
                    .Converters.Add(new DateTimeConverter());

            config.EnableSystemDiagnosticsTracing();

            config.MessageHandlers.Add(new CultureHandler());

            var rules = config.ParameterBindingRules;
            rules.Insert(0, p =>
            {
                if (p.ParameterType == typeof(Employee))
                {
                    return new AllRequestParameterBinding(p);
                }

                return null;
            });


            foreach (var formatter in config.Formatters.Where(f => f.SupportedMediaTypes
                                                .Any(m => m.MediaType.Equals(
                                                    "application/x-www-form-urlencoded"))))
            {
                Trace.WriteLine(formatter.GetType().Name);
                Trace.WriteLine("\tCanReadType Employee: " + formatter
                                                                .CanReadType(typeof(Employee)));
                Trace.WriteLine("\tCanWriteType Employee: " + formatter
                                                                .CanWriteType(typeof(Employee)));
                Trace.WriteLine("\tCanReadType FormDataCollection: " +
                                                                formatter
                                                                .CanReadType(
                                                                    typeof(FormDataCollection)));
                Trace.WriteLine("\tCanWriteType FormDataCollection: " +
                                                            formatter
                                                                .CanWriteType(
                                                                    typeof(FormDataCollection)));
                Trace.WriteLine("\tBase: " + formatter.GetType().BaseType.Name);
                Trace.WriteLine("\tMedia Types: " +
                                        string.Join(", ", formatter.SupportedMediaTypes));
            }

        }
    }
}
