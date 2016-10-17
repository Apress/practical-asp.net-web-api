using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Performant.Models
{
    public class Quote
    {
        public string Symbol { get; set; }
        public decimal Bid { get; set; }
        public decimal Ask { get; set; }
        public DateTime Time { get; set; }
    }
}