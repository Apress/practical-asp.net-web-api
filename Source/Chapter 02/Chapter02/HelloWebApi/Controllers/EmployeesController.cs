using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Tracing;
using HelloWebApi.Models;

namespace HelloWebApi.Controllers
{
    public class EmployeesController : ApiController
    {
        private readonly ITraceWriter traceWriter = null;

        public EmployeesController()
        {
            this.traceWriter = GlobalConfiguration.Configuration.Services.GetTraceWriter();
        }


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

        // GET api/employees
        //public IEnumerable<Employee> Get()
        //{
        //    return list;
        //}

        public IEnumerable<Employee> Get()
        {
            IEnumerable<Employee> employees = null;

            if (traceWriter != null)
            {
                traceWriter.TraceBeginEnd(
                    Request,
                    TraceCategories.FormattingCategory,
                    System.Web.Http.Tracing.TraceLevel.Info,
                    "EmployeesController",
                    "Get",
                    beginTrace: (tr) =>
                    {
                        tr.Message = "Entering Get";
                    },
                    execute: () =>
                    {
                        System.Threading.Thread.Sleep(1000); // Simulate delay
                        employees = list;
                    },
                    endTrace: (tr) =>
                    {
                        tr.Message = "Leaving Get";
                    },
                    errorTrace: null);
            }

            return employees;
        }



        // GET api/employees/12345
        //public Employee Get(int id)
        //{
        //    return list.First(e => e.Id == id);
        //}

        public Employee Get(int id)
        {
            var employee = list.FirstOrDefault(e => e.Id == id);

            if (traceWriter != null)
                traceWriter.Info(Request, "EmployeesController", String.Format("Getting employee {0}", id));

            if (traceWriter != null)
                traceWriter.Trace(
                    Request, "System.Web.Http.Controllers", System.Web.Http.Tracing.TraceLevel.Info,
                        (traceRecord) =>
                        {
                            traceRecord.Message =
                            String.Format("Getting employee {0}", id);

                            traceRecord.Operation = "Get(int)";
                            traceRecord.Operator = "EmployeeController";
                        });

            return employee;
        }

        // POST api/employees
        public void Post(Employee employee)
        {
            int maxId = list.Max(e => e.Id);
            employee.Id = maxId + 1;

            list.Add(employee);
        }

        // PUT api/employees/12345
        public void Put(int id, Employee employee)
        {
            int index = list.ToList().FindIndex(e => e.Id == id);
            list[index] = employee;
        }

        // DELETE api/employees/12345
        public void Delete(int id)
        {
            Employee employee = Get(id);
            list.Remove(employee);
        }
    }

}