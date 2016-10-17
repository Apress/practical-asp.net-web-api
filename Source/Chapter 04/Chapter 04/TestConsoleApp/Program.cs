using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApp
{
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        string uri = "http://localhost:45379/api/employees/12345";

    //        using (WebClient client = new WebClient())
    //        {
    //            client.Headers.Add("Accept-Encoding", "gzip, deflate;q=0.8");
    //            var response = client.DownloadString(uri);

    //            Console.WriteLine(response);
    //        }
    //    }
    //}

    class Program
    {
        static void Main(string[] args)
        {
            string uri = "http://localhost:45379/api/employees/12345";

            using (AutoDecompressionWebClient client = new AutoDecompressionWebClient())
            {
                client.Headers.Add("Accept-Encoding", "gzip, deflate;q=0.8");
                Console.WriteLine(client.DownloadString(uri));
            }
        }
    }

    class AutoDecompressionWebClient : WebClient
    {
        protected override WebRequest GetWebRequest(Uri address)
        {
            HttpWebRequest request = base.GetWebRequest(address)as HttpWebRequest;
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

            return request;
        }
    }

}
