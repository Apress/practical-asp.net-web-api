using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Robusta.TalentManager.WebApi.Dto;

namespace Robusta.TalentManager.WebApi.Client.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //HttpClient client = new HttpClient();
            HttpClient client = HttpClientFactory.Create(new CredentialsHandler());
            client.BaseAddress = new Uri("http://localhost/TalentManager/api/");

            HttpResponseMessage response = client.GetAsync("employees/1").Result;

            Console.WriteLine("{0} - {1}", (int)response.StatusCode, response.ReasonPhrase);

            if (response.IsSuccessStatusCode)
            {
                var employee = response.Content.ReadAsAsync<EmployeeDto>().Result;
                Console.WriteLine("{0}\t{1}\t{2}",
                                    employee.Id,
                                        employee.FirstName,
                                            employee.LastName,
                                                employee.DepartmentId);

            }

            EmployeeDto newEmployee = new EmployeeDto()
            {
                FirstName = "Julian",
                LastName = "Heineken",
                DepartmentId = 2
            };

            response = client.PostAsJsonAsync<EmployeeDto>
                                            ("http://localhost/TalentManager/api/employees", newEmployee)
                                                   .Result;

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);

                if (response.Headers != null)
                    Console.WriteLine(response.Headers.Location);
            }

            Console.WriteLine("{0} - {1}", (int)response.StatusCode, response.ReasonPhrase);

            client.DefaultRequestHeaders.Accept.Add(
                             new MediaTypeWithQualityHeaderValue("application/json", 0.8));

            client.DefaultRequestHeaders.Accept.Add(
                                           new MediaTypeWithQualityHeaderValue("application/xml", 0.9));

            response = client.GetAsync("employees/1").Result;

            Console.WriteLine("{0} - {1}", (int)response.StatusCode, response.ReasonPhrase);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }

        }
    }
}
