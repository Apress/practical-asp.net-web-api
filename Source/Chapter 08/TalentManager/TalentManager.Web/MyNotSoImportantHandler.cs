using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace TalentManager.Web
{
    public class MyNotSoImportantHandler : DelegatingHandler
    {
        private const string REQUEST_HEADER = "X-Name2";
        private const string RESPONSE_HEADER = "X-Message2";
        private const string NAME = "Potter";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Inspect and do your stuff with request here
            string name = String.Empty;

            if (request.Headers.Contains(REQUEST_HEADER))
            {
                name = request.Headers.GetValues(REQUEST_HEADER).First();
            }

            // If you are not happy for a reason, 
            // you can reject the request right here like this
            if (NAME.Equals(name, StringComparison.OrdinalIgnoreCase))
                return request.CreateResponse(HttpStatusCode.Forbidden);

            var response = await base.SendAsync(request, cancellationToken);

            // Inspect and do your stuff with response here
            if (response.StatusCode == HttpStatusCode.OK && !String.IsNullOrEmpty(name))
            {
                response.Headers.Add(RESPONSE_HEADER,
                    String.Format("Hello, {0}. Time is {1}",
                                name,
                                    DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt")));
            }

            return response;
        }
    }
}