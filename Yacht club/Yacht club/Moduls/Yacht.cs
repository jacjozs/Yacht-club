using System.Drawing;
using System.Windows.Media.Imaging;

namespace Yacht_club
{
    /// <summary>
    /// Yacht osztály a yacht adatok tárolására
    /// </summary>
    class Yacht
    {
        public int id { get; set; }
        public string nev { get; set; }
        public string gyarto { get; set; }
        public BitmapImage kep { get; set; }
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
        public float szeles { get; set; }
        public float hossz { get; set; }
        public float merules { get; set; }
        public int sebesseg { get; set; }
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
