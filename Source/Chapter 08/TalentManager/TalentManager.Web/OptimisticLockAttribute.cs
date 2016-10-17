using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using TalentManager.Domain;

namespace TalentManager.Web
{
    public class OptimisticLockAttribute : ActionFilterAttribute
    {
        // OnActionExecuting method goes here
        public override void OnActionExecuting(HttpActionContext context)
        {
            var request = context.Request;
            if (request.Method == HttpMethod.Put)
            {
                EntityTagHeaderValue etagFromClient = request.Headers.IfMatch.FirstOrDefault();
                if (etagFromClient != null)
                {
                    var rowVersion = Convert.FromBase64String(
                                                etagFromClient.Tag.Replace("\"", String.Empty));

                    foreach (var x in context.ActionArguments.Values.Where(v => v is IVersionable))
                    {
                        ((IVersionable)x).RowVersion = rowVersion;
                    }
                }
            }
        }

        public override void OnActionExecuted(HttpActionExecutedContext context)
        {
            var request = context.Request;

            if (request.Method == HttpMethod.Get)
            {
                object content = (context.Response.Content as ObjectContent).Value;

                if (content is IVersionable)
                {
                    byte[] rowVersion = ((IVersionable)content).RowVersion;

                    var etag = new EntityTagHeaderValue("\"" +
                                            Convert.ToBase64String(rowVersion) + "\"");

                    context.Response.Headers.ETag = etag;
                }
            }
        }
    }
}