using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace TalentManager.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var handler = new MyImportantHandler()
            {
                InnerHandler = new MyNotSoImportantHandler()
                {
                    InnerHandler = new HttpControllerDispatcher(config)
                }
            };

            config.Routes.MapHttpRoute(
                name: "premiumApi",
                routeTemplate: "premium/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: null,
                handler: handler
            );


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //config.MessageHandlers.Add(new MyImportantHandler());
            //config.MessageHandlers.Add(new MyNotSoImportantHandler());

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Never;

            config.Services.Replace(typeof(IHttpControllerSelector), new MyControllerSelector(config));
        }
    }
}
