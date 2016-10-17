using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robusta.TalentManager.Data
{
    public interface IContext : IDisposable
    {
        int SaveChanges();
    }
}
