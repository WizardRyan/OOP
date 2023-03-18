using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicycleRaceSoftware.Shared
{
    public class CheaterStamp
    {
        public int Bib { get; set; }
        public string Name { get; set; }
        public int Sensor { get; set; }
        public int Time { get; set; }

        public override string ToString()
        {
            return $"Bib {Bib} Name: {Name} Sensor: {Sensor} Time: {Time}";
        }
    }
}
