using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robusta.TalentManager.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IContext context;

        public UnitOfWork()
        {
            context = new Context();
        }

        public UnitOfWork(IContext context)
        {
            this.context = context;
        }
        public int Save()
        {
            return context.SaveChanges();
        }

        public IContext Context
        {
            get
            {
                return context;
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
