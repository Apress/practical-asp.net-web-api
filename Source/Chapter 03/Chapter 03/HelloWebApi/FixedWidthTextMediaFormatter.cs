using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HelloWebApi.Models;

namespace HelloWebApi
{
    public class FixedWidthTextMediaFormatter : MediaTypeFormatter
    {
        public FixedWidthTextMediaFormatter()
        {
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);

            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/plain"));
        }

        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            return typeof(IEnumerable<Employee>)
                                .IsAssignableFrom(type);
        }

        public override async Task WriteToStreamAsync(
                                        Type type,
                                            object value,
                                                Stream stream,
                                                    HttpContent content,
                                                        TransportContext transportContext)
        {
            using (stream)
            {
                Encoding encoding = SelectCharacterEncoding(content.Headers);

                using (var writer = new StreamWriter(stream, encoding))
                {
                    var employees = value as IEnumerable<Employee>;
                    if (employees != null)
                    {
                        foreach (var employee in employees)
                        {
                            await writer.WriteLineAsync(
                                            String.Format("{0:000000}{1,-20}{2,-20}",
                                                                employee.Id,
                                                                    employee.FirstName,
                                                                        employee.LastName));
                        }

                        await writer.FlushAsync();
                    }
                }
            }
        }
    }
}