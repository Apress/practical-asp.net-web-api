using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelloWebApi.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int Department { get; set; }
    }
}