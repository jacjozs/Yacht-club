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
        private string nev { get; set; }
        private int id { get; set; }
        private int iranyitoszm { get; set; }
        private string varos { get; set; }
        private string lakcim { get; set; }
        private Image kep { get; set; }
        private DateTime szuletesdt { get; set; }

        private DateTime utolsoLogin { get; set; }

        private int jogosultsag { get; set; }
    }
}
