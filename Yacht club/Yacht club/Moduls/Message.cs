using System;

namespace Yacht_club.Moduls
{
    /// <summary>
    /// Üzenetek és bérlések tárolása szükséges osztály
    /// </summary>
    class Message
    {
        public int uzenet_id { get; set; }
        public DateTime keletkezett { get; set; }
        public int felado_id { get; set; }
        public string felado_nev { get; set; }
        public int cimzett_id { get; set; }
        public string cimzett_nev { get; set; }
        public int yacht_id = 0;
        public string yacht_nev { get; set; }
        public int device_id = 0;
        public DateTime kezdete { get; set; }
        public DateTime vege { get; set; }
        public int honnan_id { get; set; }
        public int hova_id { get; set; }
        public bool elfogadvabl { get; set; }
        public string elfogadvastr { get; set; }
        public bool NEWbl { get; set; }
        public string NEWstr { get; set; }
        public bool BlVissza { get; set; }
        public string StrVissza { get; set; }
    }
}
