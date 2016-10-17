using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManager.Domain;

namespace TalentManager.Data
{
    public interface IEmployeeRepository : IDisposable
    {
        IEnumerable<Employee> GetByDepartment(int departmentId);

        Employee Get(int id);
    }
}
