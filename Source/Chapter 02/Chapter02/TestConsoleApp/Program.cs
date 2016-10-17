using System.Net;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string uri = "http://localhost.fiddler:65014/api/employees/12345";

            using (WebClient client = new WebClient())
            {
                client.DownloadString(uri);
            }

        }
    }
}
