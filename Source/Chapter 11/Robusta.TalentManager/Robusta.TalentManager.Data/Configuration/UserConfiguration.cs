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
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            HasKey(k => k.Id);

            Property(p => p.Id)
                .HasColumnName("user_id")
                    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.UserName).HasColumnName("user_name");
            Property(p => p.Password).HasColumnName("password");
            Property(p => p.Salt).HasColumnName("salt");
        }
    }
}
