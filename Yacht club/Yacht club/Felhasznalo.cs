﻿using System;
using System.Drawing;

namespace Yacht_club
{
    class Felhasznalo
    {
        public int member_id { get; set; }
        public string nickname { get; set; }
        public string veztek_nev { get; set; }
        public string kereszt_nev { get; set; }
        public int iranyitoszm { get; set; }
        public string varos { get; set; }
        public string lakcim { get; set; }
        public string orszag { get; set; }
        public Image kep { get; set; }
        public DateTime szuletesdt { get; set; }
        public Login login { get; set; }
    }
}
