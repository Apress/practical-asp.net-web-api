using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebCaching.Models;

namespace WebCaching.Controllers
{
    public class EmployeesController : ApiController
    {
        [Cache(MaxAgeSeconds = 6)]
        [EnableETag]
        public HttpResponseMessage GetAllEmployees()
        {
            var employees = new Employee[]
            {
                    new Employee()
                    {
                            Id = 1,
                            FirstName = "John",
                            LastName = "Human"
                    },
                    new Employee()
                    {
                            Id = 2,
                            FirstName = "Jane",
                            LastName = "Taxpayer"
                    }
            };

            var response = Request.CreateResponse<IEnumerable<Employee>>
                                                                (HttpStatusCode.OK, employees);

            return response;
        }

        [EnableETag]
        public void Post(Employee employee)
        {
            // It is okay to do nothing here for this exercise
        }


    }
}
