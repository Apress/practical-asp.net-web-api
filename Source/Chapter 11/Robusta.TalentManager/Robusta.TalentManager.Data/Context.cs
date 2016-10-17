using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robusta.TalentManager.Data.Configuration;

namespace Robusta.TalentManager.Data
{
    public class Context : DbContext, IContext
    {
        static Context()
        {
            Database.SetInitializer<Context>(null);
        }

        public Context() : base("DefaultConnection") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations
                .Add(new EmployeeConfiguration())
                .Add(new UserConfiguration());
        }
    }
}
