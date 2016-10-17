using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using HelloWebApi.Models;

namespace HelloWebApi.Controllers
{
    public class EmployeesController : ApiController
    {
        private static IList<Employee> list = new List<Employee>()
        {
            new Employee()
            {
                Id = 12345, FirstName = "John", LastName = "Human"
            },
            
            new Employee()
            {
                Id = 12346, FirstName = "Jane", LastName = "Public"
            },

            new Employee()
            {
                Id = 12347, FirstName = "Joseph", LastName = "Law"
            }
        };

        // GET api/employees/12345
        //public Employee Get(int id)
        //{
        //    return list.First(e => e.Id == id);
        //}

        //public HttpResponseMessage Get(int id)
        //{
        //    var employee = list.FirstOrDefault(e => e.Id == id);

        //    return new HttpResponseMessage()
        //    {
        //        Content = new ObjectContent<Employee>(employee, Configuration.Formatters.JsonFormatter)
        //    };
        //}

        public HttpResponseMessage Get(int id)
        {
            // hard-coded for illustration but for the use case described,
            // the blacklisted formatter might need to be retrieved from
            // a persistence store for the client application based on some identifier
            var blackListed = "application/xml";

            var allowedFormatters = Configuration.Formatters
                                        .Where(f => !f.SupportedMediaTypes
                                                    .Any(m => m.MediaType
                                                        .Equals(blackListed,
                                                            StringComparison.OrdinalIgnoreCase)));

            var result = Configuration.Services
                                     .GetContentNegotiator()
                                           .Negotiate(
                                                  typeof(Employee), Request, allowedFormatters);
            if (result == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.NotAcceptable);

            var employee = list.First(e => e.Id == id); // Assuming employee exists

            return new HttpResponseMessage()
            {
                Content = new ObjectContent<Employee>(
                                         employee,
                                                result.Formatter,
                                                      result.MediaType)
            };
        }

        public IEnumerable<Employee> Get()
        {
            return list;
        }
    }

}