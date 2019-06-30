using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class Range
    {
        public decimal LowerBound { get; set; }
        public decimal UpperBound { get; set; }

        public bool Contains(decimal number)
        {
            return number >= LowerBound && number <= UpperBound;
        }
    }
}
