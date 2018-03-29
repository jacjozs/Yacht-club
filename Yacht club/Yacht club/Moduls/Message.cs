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
        public int yacht_id { get; set; }
        public string yacht_nev { get; set; }
        public int device_id { get; set; }
        DateTime kezdete { get; set; }
        DateTime vege { get; set; }
        public int honnan_id { get; set; }
        public int hova_id { get; set; }
        public bool elfogadvabl { get; set; }
        public string elfogadvastr { get; set; }
        public bool NEWbl { get; set; }
        public string NEWstr { get; set; }
    }
}
