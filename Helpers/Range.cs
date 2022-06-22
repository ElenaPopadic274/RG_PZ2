using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz2.Helpers
{
    public class Range
    {
        public Range()
        {
        }

        public Range(double min, double max)
        {
            Min = min;
            Max = max;
        }

        public double Max { get; set; }
        public double Min { get; set; }

        public bool IsInRange(double number)
        {
            return Min <= number && number <= Max;
        }
    }
}
