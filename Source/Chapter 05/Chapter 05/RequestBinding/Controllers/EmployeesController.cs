using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using RequestBinding.Models;


namespace RequestBinding.Controllers
{
    public class EmployeesController : ApiController
    {
        // Exercise 5.1
        //public void Post(HttpRequestMessage req)
        //{
        //    var content = req.Content.ReadAsStringAsync().Result;
        //    int id = Int32.Parse(req.RequestUri.Segments.Last());

        //    Trace.WriteLine(content);
        //    Trace.WriteLine(id);
        //}

        // Exercise 5.2 - First Attempt
        //public void Post(HttpRequestMessage req)
        //{
        //    //var content = req.Content.ReadAsStringAsync().Result;
        //    var content = req.Content.ReadAsAsync<Employee>().Result;
        //    int id = Int32.Parse(req.RequestUri.Segments.Last());

        //    Trace.WriteLine(content.Id);
        //    Trace.WriteLine(content.FirstName);
        //    Trace.WriteLine(content.LastName);
        //    Trace.WriteLine(id);
        //}

        // Exercise 5.2 - Second Attempt
        //public void Post(int id, Employee employee)
        //{
        //    Trace.WriteLine(employee.Id);
        //    Trace.WriteLine(employee.FirstName);
        //    Trace.WriteLine(employee.LastName);
        //    Trace.WriteLine(id);
        //}

        // Exercise 5.3
        //public void Post(int id, string firstName, [FromBody]int locationId, Guid guid)
        //{
        //    Trace.WriteLine(id);
        //    Trace.WriteLine(firstName);
        //    Trace.WriteLine(locationId);
        //    Trace.WriteLine(guid);
        //}

        // Exercise 5.4
        //public void Post(int id, [FromUri]Employee employee)
        //{
        //    // Do Nothing
        //}

        // Side bar - BODY CAN ONLY BE READ ONCE
        //public void Post(int id, HttpRequestMessage req, Employee employee)
        //{
        //    var content = req.Content.ReadAsStringAsync().Result;
        //    var employeeContent = req.Content.ReadAsAsync<Employee>().Result;

        //}

        // Exercise 5.5
        //public void Post(int id, [FromUri]List<string> nickNames)
        //{
        //    Trace.WriteLine(String.Join(", ", nickNames));
        //}

        // Exercise 5.6.1
        //public void Post(FormDataCollection data)
        //{
        //    Trace.WriteLine(data.Get("firstName"));
        //    Trace.WriteLine(data.Get("lastName"));
        //}

        // Exercise 5.6.2
        //public int Post(Employee employee)
        //{
        //    return new Random().Next();
        //}

        // Exercise 5.6.3
        //public void Post([FromBody]string lastName)
        //{
        //    Trace.WriteLine(lastName);
        //}

        // Side bar
        //public void Post(List<int> numbers)
        //{
        //    // Do Nothing
        //}

        // Exercise 5.7
        //public void Post(Employee employee)
        //{
        //    // Do Nothing
        //}

        //public HttpResponseMessage Get(Shift shift)
        //{
        //    // Do something with shift

        //    var response = new HttpResponseMessage(HttpStatusCode.OK)
        //    {
        //        Content = new StringContent("")
        //    };

        //    return response;
        //}

        //public HttpResponseMessage Get([System.Web.Http.ModelBinding.ModelBinder]IEnumerable<string> ifmatch)
        //{
        //    var response = new HttpResponseMessage(HttpStatusCode.OK)
        //    {
        //        Content = new StringContent(ifmatch.First().ToString())
        //    };

        //    return response;
        //}

        public HttpResponseMessage Get([ModelBinder(typeof(TalentScoutModelBinderProvider))]TalentScout scout)
        {
            // Do your logic with scout model
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("")
            };

            return response;
        }

        public void Put(int id, Employee employee)
        {
            // Does nothing!
        }

    }
}
