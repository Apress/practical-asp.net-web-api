using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace HelloWebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //    config.Routes.MapHttpRoute(
            //    name: "RpcApi",
            //    routeTemplate: "api/{controller}/{action}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "webapi/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            //config.Routes.MapHttpRoute(
            //                 name: "DefaultApi",
            //                 routeTemplate: "api/{orgid}/{controller}/{id}",
            //                 defaults: new { id = RouteParameter.Optional },
            //                 constraints: new { orgid = @"\d+" }
            //          );


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
