using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        private Dictionary<string, int> list;
        /// <summary>
        /// Adatbázis béldány az adatbázis eléréséhez
        /// </summary>
        private Database.MysqlDevice data;
        public ucSzallito_add()
        {
            InitializeComponent();
            login_name();
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
                newDevice = new Device();;
                newDevice.tipus = tbSzallito_tipus.Text;
                newDevice.member_id = int.Parse(list[cbDevice_tulaj.Text].ToString());
                newDevice.max_suly = int.Parse(tbSzallito_teherb.Text);
                newDevice.max_szeles = int.Parse(tbSzallito_szeles.Text);
                newDevice.max_hossz = int.Parse(tbSzallito_hossz.Text);
                newDevice.max_magas = int.Parse(tbSzallito_magas.Text);
                newDevice.id = data.MysqlAddDevice(newDevice);
                if (newDevice.id != 0)
                    ///Visszajelzés ha sikeres volt a hozzáadás
                    AddYachtLog("ID: " + newDevice.id + "  Szállitóeszköz Hozzáadva!");
                else Globals.log = "Hozzáadás Sikertelen! <Szállitóeszköz>";
            }
            catch (Exception)
            {
                Globals.log = "Hozzáadás Sikertelen! <Szállitóeszköz>";
            }
            finally
            {
                Globals.log = "Hozzáadás Sikeres! <Szállitóeszköz>";
            }
            ///Az egyes usercontrolhoz caló log hozzáadás
            Globals.Main.logAdd(true);
        }
        /// <summary>
        /// Teljes nevek és member_id kigyüjtése és hozzá adása a legördülő sávhoz
        /// </summary>
        public void login_name()
        {
            data = new Database.MysqlDevice();
            list = data.MysqlDeviceLoginName();
            cbDevice_tulaj.ItemsSource = list.Keys;
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
