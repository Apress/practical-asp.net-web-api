using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Robusta.TalentManager.WebApi.Core.Handlers
{
    public class AuthenticationHandler : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                        CancellationToken cancellationToken)
        {
            if (request.Headers.Contains("X-PSK"))
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "jqhuman")
            };

                var principal = new ClaimsPrincipal(new[] { new ClaimsIdentity(claims, "dummy") });

                Thread.CurrentPrincipal = principal;
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
