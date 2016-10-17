using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robusta.TalentManager.Domain
{
    public class Employee : IIdentifiable, IVersionable
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int DepartmentId { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
