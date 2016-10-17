using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using Performant.Models;

namespace Performant.Controllers
{
    public class ValuesController : ApiController
    {
        //public string Get(int id)
        //{
        //    return ReadFile();
        //}

        //private string ReadFile()
        //{
        //    using (StreamReader reader = File.OpenText(@"C:\SomeFile.txt"))
        //    {
        //        Thread.Sleep(500);
        //        return reader.ReadToEnd();
        //    }
        //}

        //public async Task<string> Get(int id)
        //{
        //    return await ReadFileAsync();
        //}

        //private async Task<string> ReadFileAsync()
        //{
        //    using (StreamReader reader = File.OpenText(@"C:\SomeFile.txt"))
        //    {
        //        await Task.Delay(500);
        //        return await reader.ReadToEndAsync();
        //    }
        //}

        private static readonly Lazy<Timer> timer = new Lazy<Timer>(() => new Timer(TimerCallback, null, 0, 2000));
        private static readonly
                       ConcurrentDictionary<StreamWriter, StreamWriter> subscriptions =
                                                 new ConcurrentDictionary<StreamWriter, StreamWriter>();

        public HttpResponseMessage Get()
        {
            Timer t = timer.Value;

            Request.Headers.AcceptEncoding.Clear();

            HttpResponseMessage response = Request.CreateResponse();
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Content = new PushStreamContent(OnStreamAvailable, "text/event-stream");
            return response;
        }

        private static void OnStreamAvailable(Stream stream, HttpContent headers, TransportContext context)
        {
            StreamWriter writer = new StreamWriter(stream);
            subscriptions.TryAdd(writer, writer);
        }

        private static void TimerCallback(object state)
        {
            Random random = new Random();

            // Call the service to get the quote - hardcoding the quote here
            Quote quote = new Quote()
            {
                Symbol = "CTSH",
                Bid = random.Next(70, 72) + Math.Round((decimal)random.NextDouble(), 2),
                Ask = random.Next(71, 73) + Math.Round((decimal)random.NextDouble(), 2),
                Time = DateTime.Now
            };

            string payload = "data:" + JsonConvert.SerializeObject(quote) + "\n\n";

            foreach (var pair in subscriptions.ToArray())
            {
                StreamWriter writer = pair.Value;

                try
                {
                    writer.Write(payload);
                    writer.Flush();
                }
                catch
                {
                    StreamWriter disconnectedWriter;
                    subscriptions.TryRemove(writer, out disconnectedWriter);

                    if (disconnectedWriter != null)
                        disconnectedWriter.Close();
                }
            }
        }
    }
}