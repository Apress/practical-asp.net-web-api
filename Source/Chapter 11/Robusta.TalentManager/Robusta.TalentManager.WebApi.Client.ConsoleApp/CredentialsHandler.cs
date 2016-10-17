using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Robusta.TalentManager.WebApi.Client.ConsoleApp
{
    public class CredentialsHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
                                        HttpRequestMessage request,
                                            CancellationToken cancellationToken)
        {
            var headers = request.Headers;
            if (headers.Authorization == null)
            {
                string creds = String.Format("{0}:{1}", "jqhuman", "p@ssw0rd!");
                byte[] bytes = Encoding.Default.GetBytes(creds);

                var header = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytes));
                headers.Authorization = header;
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
