using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicycleRaceSoftware.Shared
{
    public class Racer : IComparable<Racer>
    {
        public string Name { get; set; }
        public int Bib { get; set; }
        public int GroupNum { get; set; }

        public Racer()
        {
            Name = "NAME_NOT_FOUND";
        }

        public override string ToString()
        {
            return "Name: " + Name + " Bib: " + Bib + " GroupNum: " + GroupNum;
        }

        public static bool operator<(Racer r1, Racer r2)
        {
            return r1.Bib < r2.Bib;
        }
        public static bool operator>(Racer r1, Racer r2)
        {
            return r1.Bib > r2.Bib;
        }
        public int CompareTo(Racer r)
        {
            return Bib < r.Bib ? -1 : 1;
        }

	}
}
