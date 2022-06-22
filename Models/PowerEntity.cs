using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz2.Models
{
    public class PowerEntity
    {
        private int connections;


        public long Id { get; set; }

        public string Name { get; set; }

        public double X { get; set; }

        public double Y { get; set; }
        public int Connections {
            get { return connections; }
            set { connections = value; } 
        }

        public override string ToString()
        {
            return $"Id={Id}, Name={Name}, Type={GetType()}";
        }
    }
}
