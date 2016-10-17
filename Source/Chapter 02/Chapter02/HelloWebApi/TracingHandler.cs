using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace HelloWebApi
{
    public class TracingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                                            CancellationToken cancellationToken)
        {
            HttpMessageContent requestContent = new HttpMessageContent(request);
            string requestMessage = await requestContent.ReadAsStringAsync();

            var response = await base.SendAsync(request, cancellationToken);

            HttpMessageContent responseContent = new HttpMessageContent(response);
            string responseMessage = await responseContent.ReadAsStringAsync();

            GlobalConfiguration.Configuration.Services.GetTraceWriter()
                .Trace(request, "System.Web.Http.MessageHandlers", System.Web.Http.Tracing.TraceLevel.Info,
                    (t) =>
                    {
                        t.Message = String.Format("\n{0}\n{1}\n", requestMessage, responseMessage);
                    });

            return response;
        }
    }
}