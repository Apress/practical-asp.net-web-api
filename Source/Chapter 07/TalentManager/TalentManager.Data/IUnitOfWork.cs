using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentManager.Data
{
    public interface IUnitOfWork : IDisposable
    {
        int Save();
        IMyContext Context { get; }
    }
}
