using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceModel.Security.Tokens;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Robusta.TalentManager.WebApi.Core.Handlers
{
    public class JwtHandler : DelegatingHandler
    {
        private const string ISSUER = "Robusta.Broker";
        private const string AUDIENCE = "http://localhost/talentmanager/api";

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                                                              CancellationToken cancellationToken)
        {
            byte[] key = Convert.FromBase64String("qqO5yXcbijtAdYmS2Otyzeze2XQedqy+Tp37wQ3sgTQ=");

            try
            {
                var headers = request.Headers;
                if (headers.Authorization != null)
                {
                    if (headers.Authorization.Scheme.Equals("Bearer"))
                    {
                        string jwt = request.Headers.Authorization.Parameter;

                        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                        TokenValidationParameters parms = new TokenValidationParameters()
                        {
                            AllowedAudience = AUDIENCE,
                            ValidIssuers = new List<string>() { ISSUER },
                            SigningToken = new BinarySecretSecurityToken(key)
                        };

                        var principal = tokenHandler.ValidateToken(jwt, parms);

                        Thread.CurrentPrincipal = principal;

                        if (HttpContext.Current != null)
                            HttpContext.Current.User = principal;
                    }
                }

                var response = await base.SendAsync(request, cancellationToken);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    response.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue("Bearer", "error=\"invalid_token\""));
                }
                return response;
            }
            catch (Exception)
            {
                var response = request.CreateResponse(HttpStatusCode.Unauthorized);
                response.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue("Bearer", "error=\"invalid_token\""));

                return response;
            }
        }
    }
}
