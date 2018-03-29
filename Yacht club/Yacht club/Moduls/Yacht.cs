using System.Drawing;

namespace Yacht_club
{
    /// <summary>
    /// Yacht osztály a yacht adatok tárolására
    /// </summary>
    class Yacht
    {
        public int id { get; set; }
        public string nev { get; set; }
        public string tipus { get; set; }
        public Image kep { get; set; }
        public int napi_ar { get; set; }
        /// <summary>
        /// tulajdonos member_id
        /// </summary>
        public int member_id { get; set; }
        /// <summary>
        /// tulajdonos teljesnév
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
        public int ferohely { get; set; }
        public int szeles { get; set; }
        public int hossz { get; set; }
        public int magas { get; set; }
        public int suly { get; set; }
        public int kikoto_id { get; set; }
        public string kikoto { get; set; }
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
