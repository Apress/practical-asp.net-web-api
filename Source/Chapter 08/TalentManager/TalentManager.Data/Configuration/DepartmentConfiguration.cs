using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalentManager.Domain;

namespace TalentManager.Data.Configuration
{
    public class DepartmentConfiguration : EntityTypeConfiguration<Department>
    {
        public DepartmentConfiguration()
        {
            HasKey(k => k.Id);

            Property(p => p.Id)
                .HasColumnName("department_id")
                    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Name).HasColumnName("name");

            Property(p => p.RowVersion).HasColumnName("row_version").IsRowVersion();
        }
    }
}
