using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Yacht_club
{
    class Felhasznalo
    {
        public string nev { get; set; }
        public int id { get; set; }
        public int iranyitoszm { get; set; }
        public string varos { get; set; }
        public string lakcim { get; set; }
        public Image kep { get; set; }
        public DateTime szuletesdt { get; set; }

        public DateTime utolsoLogin { get; set; }

        public bool admin { get; set; }
    }
}
