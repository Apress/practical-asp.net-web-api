using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManager.Domain;

namespace TalentManager.Data
{
    public interface IContext
    {
        IDbSet<Employee> Employees { get; set; }
        IDbSet<Department> Departments { get; set; }
    }
}
