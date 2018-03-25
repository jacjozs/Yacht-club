using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yacht_club
{
    public static class Globals
    {
        public static String log = "";
        public static UsingControls.ucErtesitesek log_windows = new UsingControls.ucErtesitesek();
        public static MySqlConnection connect = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;database=yacht_club");
        internal static Felhasznalo User { get; set; }
    }
}
