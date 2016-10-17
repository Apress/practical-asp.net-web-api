using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RequestBinding.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime Doj { get; set; }

        public string Xaffiliation { get; set; }
    }
}