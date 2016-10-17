using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentManager.Domain
{
    public class Department : IIdentifiable, IVersionable
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
