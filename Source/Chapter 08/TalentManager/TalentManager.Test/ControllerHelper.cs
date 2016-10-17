using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;

namespace TalentManager.Test
{
    public static class ControllerHelper
    {
        public static void EnsureNotNull(this ApiController controller)
        {
            controller.Configuration = new HttpConfiguration();
            controller.Request = new HttpRequestMessage();
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey,
                                                            controller.Configuration);
        }

        public static void SetRequest(this ApiController controller, string controllerPrefix,
                                                                                        HttpMethod method, string requestUri)
        {
            controller.Configuration = new HttpConfiguration();

            var route = controller.Configuration.Routes.MapHttpRoute(
                        name: "DefaultApi",
                        routeTemplate: "api/{controller}/{id}",
                        defaults: new { id = RouteParameter.Optional }
            );

            var routeValues = new HttpRouteValueDictionary();
            routeValues.Add("controller", controllerPrefix);
            var routeData = new HttpRouteData(route, routeValues);

            controller.Request = new HttpRequestMessage(method, requestUri);
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, controller.Configuration);
            controller.Request.Properties.Add(HttpPropertyKeys.HttpRouteDataKey, routeData);
        }
    }
}
