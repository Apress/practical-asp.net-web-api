using System;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Robusta.TalentManager.WebApi.Core.Configuration;
using Robusta.TalentManager.WebApi.Dto;

namespace Robusta.TalentManager.WebApi.Test
{
    [TestClass]
    public class EmployeesControllerIntegrationTest
    {
        private HttpServer server = null;

        [TestInitialize()]
        public void Initialize()
        {
            var configuration = new HttpConfiguration();

            IocConfig.RegisterDependencyResolver(configuration);
            WebApiConfig.Register(configuration);
            DtoMapperConfig.CreateMaps();

            server = new HttpServer(configuration);

            // This test runs under the context of my user 
            // account (Windows Identity) and hence I clear the same
            Thread.CurrentPrincipal = new GenericPrincipal(
                                            new GenericIdentity(String.Empty),
                                                    null);
        }

        // Test methods go here

        [TestMethod]
        public void MustReturn401WhenNoCredentialsInRequest()
        {
            using (var invoker = new HttpMessageInvoker(server))
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get,
                                                    "http://localhost/api/employees/1"))
                {
                    using (var response = invoker.SendAsync(request,
                                                        CancellationToken.None).Result)
                    {
                        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
                    }
                }
            }
        }


        [TestMethod]
        public void MustReturn200AndEmployeeWhenCredentialsAreSupplied()
        {
            using (var invoker = new HttpMessageInvoker(server))
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get,
                                                    "http://localhost/api/employees/1"))
                {
                    request.Headers.Add("X-PSK", "somekey"); // Credentials
                    using (var response = invoker.SendAsync(request,
                                                        CancellationToken.None).Result)
                    {
                        Assert.IsNotNull(response);
                        Assert.IsNotNull(response.Content);
                        Assert.IsInstanceOfType(response.Content, typeof(ObjectContent<EmployeeDto>));
                        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

                        var content = (response.Content as ObjectContent<EmployeeDto>);
                        var result = content.Value as EmployeeDto;

                        Assert.AreEqual(1, result.Id);
                        Assert.AreEqual("Johnny", result.FirstName);
                        Assert.AreEqual("Human", result.LastName);
                    }
                }
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (server != null)
                server.Dispose();
        }
    }

}
