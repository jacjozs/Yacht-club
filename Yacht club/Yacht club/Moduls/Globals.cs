using MySql.Data.MySqlClient;
using System;
using System.Windows.Controls;
using Yacht_club.Moduls;

namespace Yacht_club
{
    /// <summary>
    /// Globális adatok a könnyeb adat elérésért
    /// </summary>
    public static class Globals
    {
        public static string log = "";
        public static UsingControls.ucLog log_windows = new UsingControls.ucLog();
        public static MySqlConnection connect = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;database=gamfyachtclub");
        public static Main_Yacht_Window Main;
        public static UserControl History;
        internal static Themes MainTheme;
        internal static Felhasznalo User;
        internal static Yacht selectedYacht;
        internal static Device selectedDevice;
        internal static Message selectedMessage;

        public static void UpdateHistory()
        {
            if (Main != null)
            {
                History = (UserControl)Main.ccWindow_Main.Content;
            }
        }
    }
}
