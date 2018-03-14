using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Yacht_club
{
    class Yacht
    {
        private string nev { get; set; }
        private int id { get; set; }
        private string tulaj { get; set; }
        private string berlo { get; set; }
        private int ferohely { get; set; }
        private string foglalt { get; set; }
        private Image kep { get; set; }

        public Yacht(string nev, int id, string tulaj, string berlo, int ferohely, bool foglalt, Image kep)
        {
            this.nev = nev;
            this.id = id;
            this.tulaj = tulaj;
            this.berlo = berlo;
            this.ferohely = ferohely;
            this.foglalt = foglalt? "Foglalt" : "Szabad";
            if (kep != null)
            {
                this.kep = kep;
            }
            else this.kep = null;
        }

    }
}
