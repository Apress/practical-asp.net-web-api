using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Robusta.TalentManager.Data;
using Robusta.TalentManager.Domain;

namespace Robusta.TalentManager.WebApi.Core.Handlers
{
    //public class AuthenticationHandler : DelegatingHandler
    //{
    //    protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
    //                                                    CancellationToken cancellationToken)
    //    {
    //        if (request.Headers.Contains("X-PSK"))
    //        {
    //            var claims = new List<Claim>
    //        {
    //            new Claim(ClaimTypes.Name, "jqhuman")
    //        };

    //            var principal = new ClaimsPrincipal(new[] { new ClaimsIdentity(claims, "dummy") });

    //            Thread.CurrentPrincipal = principal;
    //        }

    //        return await base.SendAsync(request, cancellationToken);
    //    }
    //}

    public class AuthenticationHandler : DelegatingHandler
    {
        private const string SCHEME = "Basic";
        private readonly IRepository<User> repository = null;

        public AuthenticationHandler(IRepository<User> repository)
        {
            this.repository = repository;
        }

        protected async override Task<HttpResponseMessage> SendAsync(
                                                                      HttpRequestMessage request,
                                                                               CancellationToken cancellationToken)
        {
            try
            {
                // Perform request processing here

                var headers = request.Headers;
                if (headers.Authorization != null && SCHEME.Equals(headers.Authorization.Scheme))
                {
                    Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                    string credentials = encoding.GetString(
                        Convert.FromBase64String(headers.Authorization.Parameter));

                    string[] parts = credentials.Split(':');
                    string userName = parts[0].Trim();
                    string password = parts[1].Trim();

                    User user = repository.All.FirstOrDefault(u => u.UserName == userName);
                    if (user != null && user.IsAuthentic(password))
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, userName)
                        };

                        var principal = new ClaimsPrincipal(new[] {
                                                new ClaimsIdentity(claims, SCHEME) });

                        Thread.CurrentPrincipal = principal;

                        if (HttpContext.Current != null)
                            HttpContext.Current.User = principal;
                    }
                }


                var response = await base.SendAsync(request, cancellationToken);

                // Perform response processing here

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    response.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue(SCHEME));
                }


                return response;
            }
            catch (Exception)
            {
                // Perform error processing here

                var response = request.CreateResponse(HttpStatusCode.Unauthorized);
                response.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue(SCHEME));
                return response;

            }
        }
    }
}
