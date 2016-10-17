using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.SelfHost;
using Robusta.TalentManager.WebApi.Core.Configuration;

namespace Robusta.TalentManager.WebApi.SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            //var configuration = new HttpSelfHostConfiguration("http://localhost:8086");

            var configuration = new MySelfHostConfiguration("https://localhost:8086");

            WebApiConfig.Register(configuration);
            DtoMapperConfig.CreateMaps();
            IocConfig.RegisterDependencyResolver(configuration);

            using (HttpSelfHostServer server = new HttpSelfHostServer(configuration))
            {
                server.OpenAsync().Wait();
                Console.WriteLine("Press Enter to terminate the server...");
                Console.ReadLine();
            }
        }
    }
}
