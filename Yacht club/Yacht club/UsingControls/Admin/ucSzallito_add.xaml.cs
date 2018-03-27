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
        private Device newDevice;
        private Dictionary<string, int> list;
        private Database.MysqlDevice data;
        public ucSzallito_add()
        {
            InitializeComponent();
            login_name();
        }

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
            Globals.Main.logAdd(true);
        }

        public void login_name()
        {
            data = new Database.MysqlDevice();
            list = data.MysqlDeviceLoginName();
            cbDevice_tulaj.ItemsSource = list.Keys;
        }

        private void AddYachtLog(string log)
        {
            lbDeviceLog.Items.Insert(0, log);
        }
    }
}
