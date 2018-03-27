using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Yacht_club
{
    class Yacht
    {
        public int id { get; set; }
        public string nev { get; set; }
        public string tipus { get; set; }
        public Image kep { get; set; }
        public int napi_ar { get; set; }
        public int member_id { get; set; }
        public string full_name { get; set; }
        public int berlo_id { get; set; }
        public string berlo_full_name { get; set; }
        public int ferohely { get; set; }
        public int szeles { get; set; }
        public int hossz { get; set; }
        public int magas { get; set; }
        public int suly { get; set; }
        public int kikoto_id { get; set; }
        public string kikoto { get; set; }
        public bool blfoglalt { get; set; }
        public string strfoglalt { get; set; }
        public bool berelheto { get; set; }
    }
}
