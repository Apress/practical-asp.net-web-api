using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace TalentManager.Web
{
    public class MyControllerSelector : DefaultHttpControllerSelector
    {
        public MyControllerSelector(HttpConfiguration configuration) : base(configuration) { }

        public override string GetControllerName(HttpRequestMessage request)
        {
            string controllerName = base.GetControllerName(request);

            // Our customization goes here

            // Having controllers like EmployeesV2Controller or EmployeesV3Controller is
            // our internal business.
            // A client must not make a request directly like /api/employeesv2
            int version;
            int length = controllerName.Length;
            if (Int32.TryParse(controllerName.Substring(length - 1, 1), out version))
            {
                if (controllerName.Substring(length - 2, 1)
                                                               .Equals("V", StringComparison.OrdinalIgnoreCase))
                {
                    string message = "No HTTP resource was found that matches the request URI {0}";

                    throw new HttpResponseException(
                                request.CreateErrorResponse(
                                    HttpStatusCode.NotFound,
                                        String.Format(message, request.RequestUri)));
                }
            }

            // If client requests for a specific version through the request header, we entertain it
            if (request.Headers.Contains("X-Version"))
            {
                string headerValue = request.Headers.GetValues("X-Version").First();

                if (!String.IsNullOrEmpty(headerValue) &&
                                        Int32.TryParse(headerValue, out version))
                {
                    controllerName = String.Format("{0}v{1}", controllerName, version);

                    HttpControllerDescriptor descriptor = null;
                    if (!this.GetControllerMapping().TryGetValue(controllerName, out descriptor))
                    {
                        string message = "No HTTP resource was found that matches the request URI {0} and version {1}";

                        throw new HttpResponseException(
                                    request.CreateErrorResponse(
                                        HttpStatusCode.NotFound,
                                            String.Format(message, request.RequestUri, version)));
                    }
                }
            }

            return controllerName;
        }
    }
}