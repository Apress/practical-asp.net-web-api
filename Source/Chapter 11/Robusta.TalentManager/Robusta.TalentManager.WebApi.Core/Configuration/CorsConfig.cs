using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Thinktecture.IdentityModel.Http.Cors.WebApi;

namespace Robusta.TalentManager.WebApi.Core.Configuration
{
    public static class CorsConfig
    {
        public static void RegisterCors(HttpConfiguration httpConfig)
        {
            WebApiCorsConfiguration corsConfig = new WebApiCorsConfiguration();

            corsConfig.RegisterGlobal(httpConfig);

            corsConfig
                .ForResources("Employees")
                    .ForOrigins("http://localhost:14126")
                        .AllowRequestHeaders("Authorization")
                            .AllowMethods("GET");
        }
    }
}
