using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robusta.TalentManager.Domain;

namespace Robusta.TalentManager.Data.Configuration
{
    public class EmployeeConfiguration : EntityTypeConfiguration<Employee>
    {
        public EmployeeConfiguration()
        {
            HasKey(k => k.Id);

            Property(p => p.Id)
                .HasColumnName("employee_id")
                    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.FirstName).HasColumnName("first_name");
            Property(p => p.LastName).HasColumnName("last_name");
            Property(p => p.DepartmentId).HasColumnName("department_id");

            Property(p => p.RowVersion).HasColumnName("row_version").IsRowVersion();
        }
    }
}
