using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TalentManager.Domain;

namespace TalentManager.Web.Models
{
    public class EmployeeDto : IVersionable
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int DepartmentId { get; set; }

        public byte[] RowVersion { get; set; }
    }
}