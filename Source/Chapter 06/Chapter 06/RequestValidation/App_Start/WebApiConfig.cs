using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Validation;
using System.Web.Http.Validation.Providers;

namespace RequestValidation
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

            config.Services.RemoveAll(typeof(ModelValidatorProvider), v => v is InvalidModelValidatorProvider);

            config.Filters.Add(new ValidationErrorHandlerFilterAttribute());

            config.MessageHandlers.Add(new CultureHandler());
        }
    }
}
