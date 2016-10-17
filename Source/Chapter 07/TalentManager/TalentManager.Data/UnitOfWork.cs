using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentManager.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMyContext context;

        public UnitOfWork()
        {
            context = new MyContext();
        }

        public UnitOfWork(IMyContext context)
        {
            this.context = context;
        }
        public int Save()
        {
            return context.SaveChanges();
        }

        public IMyContext Context
        {
            get
            {
                return context;
            }
        }

        public void Dispose()
        {
            if (context != null)
                context.Dispose();
        }
    }
}
