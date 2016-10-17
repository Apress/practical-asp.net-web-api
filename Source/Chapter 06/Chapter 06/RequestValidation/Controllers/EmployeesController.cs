using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RequestValidation.Models;

namespace RequestValidation.Controllers
{
    public class EmployeesController : ApiController
    {
        public void Post(Employee employee)
        {
            if (ModelState.IsValid)
            {
                // Just be happy and do nothing
            }
            else
            {
                var errors = ModelState.Where(e => e.Value.Errors.Count > 0)
                                    .Select(e => new
                                    {
                                        Name = e.Key,
                                        Message = e.Value.Errors.First().ErrorMessage,
                                        Exception = e.Value.Errors.First().Exception
                                    }).ToList();
            }
        }
    }
}
