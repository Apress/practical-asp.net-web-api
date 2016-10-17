using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManager.Data.Configuration;

namespace TalentManager.Data
{
    public class MyContext : DbContext, IMyContext
    {
        static MyContext()
        {
            Database.SetInitializer<MyContext>(null);
        }

        public MyContext() : base("DefaultConnection") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations
                .Add(new EmployeeConfiguration())
                .Add(new DepartmentConfiguration());
        }
    }
}
