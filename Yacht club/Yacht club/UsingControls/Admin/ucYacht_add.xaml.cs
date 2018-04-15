using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Yacht_club.Database;
using Yacht_club.Moduls;

namespace Yacht_club.UsingControls
{
    /// <summary>
    /// Interaction logic for ucYacht_add.xaml
    /// </summary>
    public partial class ucYacht_add : UserControl
    {
        /// <summary>
        /// Az új yacht adatainak tárolása
        /// </summary>
        private Yacht newYacht;
        /// <summary>
        /// Egy "map" a member_id és teljesnévnek
        /// A teljesnév a kulcs!
        /// </summary>
        private Dictionary<int, string> list;
        /// <summary>
        /// Adatbázis példány az adatbázis használatához
        /// </summary>
        private MysqlYacht data;
        public ucYacht_add()
        {
            InitializeComponent();
            login_name();
            Globals.UpdateHistory();
        }
        /// <summary>
        /// A beírt adatok eltárolása és átadása az adatbázisnak
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_Hozza_add(object sender, RoutedEventArgs e)
        {
            try
            {
                data = new MysqlYacht();
                newYacht = new Yacht();
                newYacht.nev = tbYacht_nev.Text;
                newYacht.gyarto = tbYacht_gyarto.Text;
                newYacht.member_id = TbYacht_tulaj.ID;
                newYacht.ferohely = int.Parse(tbYacht_ferohely.Text);
                newYacht.sebesseg = int.Parse(tbYacht_sebesseg.Text);
                newYacht.szeles = (float)Convert.ToDouble(tbYacht_szeles.Text);
                newYacht.hossz = (float)Convert.ToDouble(tbYacht_hossz.Text);
                newYacht.merules = (float)Convert.ToDouble(tbYacht_merules.Text);
                ///Sikeres adatbeirás után visszajelzés az egyes usercontrolban valamint a hozzáadások táblában
                if (data.MysqlAddYacht(newYacht))
                {
                    AddYachtLog(newYacht.nev + " Hozzáadva!");
                    Globals.log = "Hozzáadás Sikeres! <Yacht>";
                }
                else Globals.log = "Hozzáadás Sikertelen! <Yacht>";
            }
            catch (Exception)
            {
                Globals.log = "Hozzáadás Sikertelen! <Yacht>";
            }
            finally
            {
                ///textboxok leüritése
                tbYacht_nev.Text = "";
                tbYacht_gyarto.Text = "";
                tbYacht_ferohely.Text = "";
                tbYacht_sebesseg.Text = "";
                tbYacht_szeles.Text = "";
                tbYacht_hossz.Text = "";
                tbYacht_merules.Text = "";
            }
            Globals.Main.logAdd(true);
        }
        /// <summary>
        /// Teljes nevek és member_id kigyüjtése és hozzá adása a legördülő sávhoz
        /// </summary>
        public void login_name()
        {
            list = MysqlGeneral.MysqlLoginName();
            foreach (var entry in list)
            {
                TbYacht_tulaj.AddItem(new AutoCompleteEntry(entry.Key, entry.Value, Globals.cut(entry.Value)));
            }
        }
        /// <summary>
        /// Sikeres hozzáadás bejegyzése a hozzáadások ablakhoz
        /// </summary>
        /// <param name="log"></param>
        private void AddYachtLog(string log)
        {
            lbYachtLog.Items.Insert(0, log);
        }
    }
}
