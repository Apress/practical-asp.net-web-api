using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RequestBinding.Models
{
    public class TalentScout
    {
        public IList<string> Departments { get; set; }
        public bool IsCtcBased { get; set; }
        public DateTime Doj { get; set; }
    }
}