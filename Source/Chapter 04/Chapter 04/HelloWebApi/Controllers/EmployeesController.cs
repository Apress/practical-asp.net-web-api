using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
                //LastName = "ようこそいらっしゃいました。"
                , Compensation = 45678.12M
                , Doj = new DateTime(1990, 06, 02)
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

        //public Employee Get(int id)
        //{
        //    return list.First(e => e.Id == id);
        //}

        public Employee Get(int id)
        {
            var employee = list.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                var response = Request.CreateResponse(HttpStatusCode.NotFound,
                                                                new HttpError(
                                                                        Resources.Messages.NotFound));

                throw new HttpResponseException(response);
            }

            return employee;
        }

    }
}
