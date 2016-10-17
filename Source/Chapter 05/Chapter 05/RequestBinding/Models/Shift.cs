using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace RequestBinding.Models
{
    [TypeConverter(typeof(ShiftTypeConverter))]
    public class Shift
    {
        public DateTime Date { get; set; }

        public TimeSpan Start { get; set; }

        public TimeSpan End { get; set; }
    }
}