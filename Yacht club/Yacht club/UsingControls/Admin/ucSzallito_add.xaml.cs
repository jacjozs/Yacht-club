using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Yacht_club.Moduls;

namespace Yacht_club.UsingControls
{
    /// <summary>
    /// Interaction logic for ucSzallitok_add.xaml
    /// </summary>
    public partial class ucSzallito_add : UserControl
    {
        /// <summary>
        /// új szállitóeszköz adatainak eltárolása
        /// </summary>
        private Device newDevice;
        /// <summary>
        /// "map" a felhasználók teljes nevével és member_id-jével
        /// a teljesnév a kulcs!s
        /// </summary>
        private Dictionary<int, string> list;
        /// <summary>
        /// Adatbázis béldány az adatbázis eléréséhez
        /// </summary>
        private Database.MysqlDevice data;
        public ucSzallito_add()
        {
            InitializeComponent();
            login_name();
            Globals.UpdateHistory();
        }
        /// <summary>
        /// szállitoeszköz adatainak bejügytése egy objectumba és annak átadása az adatbázisnak
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_Hozza_add(object sender, RoutedEventArgs e)
        {
            try
            {
                newDevice = new Device(); ;
                newDevice.tipus = tbSzallito_tipus.Text;
                newDevice.member_id = TbDevice_tulaj.ID;
                newDevice.max_suly = int.Parse(tbSzallito_teherb.Text);
                newDevice.max_hossz = tbSzallito_hossz.Text;
                newDevice.id = data.MysqlAddDevice(newDevice);
                if (newDevice.id != 0)
                {
                    ///Visszajelzés ha sikeres volt a hozzáadás
                    AddYachtLog("ID: " + newDevice.id + "  Szállitóeszköz Hozzáadva!");
                    Globals.log = "Hozzáadás Sikeres! <Szállitóeszköz>";
                }
                else Globals.log = "Hozzáadás Sikertelen! <Szállitóeszköz>";
            }
            catch (Exception)
            {
                Globals.log = "Hozzáadás Sikertelen! <Szállitóeszköz>";
            }
            finally
            {
                ///Az egyes usercontrolhoz való log hozzáadás
                Globals.Main.logAdd(true);
            }
        }
        /// <summary>
        /// Teljes nevek és member_id kigyüjtése és hozzá adása a legördülő sávhoz
        /// </summary>
        public void login_name()
        {
            data = new Database.MysqlDevice();
            list = data.MysqlDeviceLoginName();
            foreach (var entry in list)
            {
                TbDevice_tulaj.AddItem(new AutoCompleteEntry(entry.Key, entry.Value, Globals.cut(entry.Value)));
            }
        }
        /// <summary>
        /// sikeres yacht hozzáadás visszajelzése
        /// </summary>
        /// <param name="log">visszajelzés szövege</param>
        private void AddYachtLog(string log)
        {
            lbDeviceLog.Items.Insert(0, log);
        }
    }
}
