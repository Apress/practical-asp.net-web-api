using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Robusta.TalentManager.Data;
using Robusta.TalentManager.Domain;
using Robusta.TalentManager.WebApi.Core.Handlers;

namespace Robusta.TalentManager.WebApi.Core.Configuration
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

            //config.MessageHandlers.Add(new AuthenticationHandler());

            var repository = config.DependencyResolver
                            .GetService(typeof(IRepository<User>))
                                                    as IRepository<User>;

            config.MessageHandlers.Add(new AuthenticationHandler(repository));
            config.MessageHandlers.Add(new JwtHandler());
        }
    }
}
