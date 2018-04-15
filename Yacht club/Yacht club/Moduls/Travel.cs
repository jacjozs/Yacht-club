using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yacht_club.Moduls
{
    class Travel
    {
        public int utazas_id { get; set; }
        public DateTime mikor { get; set; }
        /// <summary>
        /// message_id
        /// </summary>
        public int berles_id { get; set; }
        public int kikoto_id { get; set; }
        public string kikoto_nev { get; set; }
        public int berlo_id { get; set; }
        public string berlo_nev { get; set; }
    }
}
