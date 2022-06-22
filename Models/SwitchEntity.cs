using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz2.Models
{
    public class SwitchEntity : PowerEntity
    {
        public string Status { get; set; }

        public override string ToString()
        {
            return $"Id={Id}, Name={Name}, Status={Status}, Type={GetType()}";
        }
       
    }
}
