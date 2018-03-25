﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Yacht_club
{
    class Yacht
    {
        public string nev { get; set; }
        public int id { get; set; }
        public string tulaj { get; set; }
        public string berlo { get; set; }
        public int ferohely { get; set; }
        public string foglalt { get; set; }
        public Image kep { get; set; }

        public Yacht(string nev, int id, string tulaj, string berlo, int ferohely, bool foglalt, Image kep)
        {
            this.nev = nev;
            this.id = id;
            this.tulaj = tulaj;
            this.berlo = berlo;
            this.ferohely = ferohely;
            this.foglalt = foglalt ? "Foglalt" : "Szabad";
            if (kep != null)
            {
                this.kep = kep;
            }
            else this.kep = null;
        }
    }
}
