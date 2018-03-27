using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yacht_club
{
    /// <summary>
    /// Szállitóeszközök osztály az adatok tárolására
    /// </summary>
    class Device
    {
        public int id { get; set; }
        public string tipus { get; set; }
        public int napi_ar { get; set; }
        /// <summary>
        /// tulajdonos member_id-e
        /// </summary>
        public int member_id { get; set; }
        /// <summary>
        /// tulajdonos teljes neve
        /// </summary>
        public string full_name { get; set; }
        /// <summary>
        /// bétlő id-e
        /// </summary>
        public int berlo_id { get; set; }
        /// <summary>
        /// bérlő teljes neve
        /// </summary>
        public string berlo_full_name { get; set; }
        public int max_hossz { get; set; }
        public int max_magas { get; set; }
        public int max_suly { get; set; }
        public int max_szeles { get; set; }
        /// <summary>
        /// foglalt bool érték a programnak
        /// </summary>
        public bool blfoglalt { get; set; }
        /// <summary>
        /// foglalt string a listázásokhoz
        /// </summary>
        public string strfoglalt { get; set; }
        public bool berelheto { get; set; }
    }
}
