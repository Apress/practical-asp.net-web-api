using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static async void RunClient()
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync("http://localhost:35868/api/values",
                                                                                   HttpCompletionOption.ResponseHeadersRead);

            using (Stream stream = await response.Content.ReadAsStreamAsync())
            {
                byte[] buffer = new byte[512];
                int bytesRead = 0;

                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    string content = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine(content);
                }
            }
        }

        static void Main(string[] args)
        {
            RunClient();

            Console.WriteLine("Press ENTER to Close");
            Console.ReadLine();
        }
    }
}
