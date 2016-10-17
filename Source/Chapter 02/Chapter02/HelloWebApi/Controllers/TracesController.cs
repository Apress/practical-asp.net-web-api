using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Tracing;

namespace HelloWebApi.Controllers
{
    public class TracesController : ApiController
    {
        public HttpResponseMessage Get()
        {
            StringBuilder content = new StringBuilder();

            var entries = RingBufferLog.Instance.PeekAll();
            if (entries != null && entries.Count > 0)
            {
                int indent = 0;

                foreach (var entry in entries)
                {
                    if (!String.IsNullOrEmpty(entry.Operation) &&
                                !String.IsNullOrEmpty(entry.Operator) &&
                                    !String.IsNullOrEmpty(entry.Category))
                    {
                        if (entry.Kind == TraceKind.Begin)
                        {
                            var end = entries.FirstOrDefault(e => entry.RequestId.Equals(e.RequestId) &&
                                                                    entry.Operator.Equals(e.Operator) &&
                                                                        entry.Operation.Equals(e.Operation) &&
                                                                            entry.Category.Equals(e.Category) &&
                                                                                e.Kind == TraceKind.End);
                            string millis = String.Empty;

                            if (end != null)
                                millis = (end.Timestamp - entry.Timestamp).TotalMilliseconds.ToString();

                            content.Append('\t', indent);
                            content.AppendFormat("BGN {0} {1} {2} {3}\n",
                                                                  entry.RequestId, entry.Operator, entry.Operation, millis);

                            indent++;
                        }
                        else
                        {
                            indent--;

                            content.Append('\t', indent);
                            content.AppendFormat("END {0} {1} {2}\n",
                                                                            entry.RequestId, entry.Operator, entry.Operation);
                        }
                    }
                }
            }

            return new HttpResponseMessage()
            {
                Content = new StringContent(content.ToString())
            };
        }

    }
}
