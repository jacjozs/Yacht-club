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
    /// Interaction logic for ucSzallito_berles.xaml
    /// </summary>
    public partial class ucSzallito_berles : UserControl
    {
        Database.MysqlDevice data;
        public ucSzallito_berles()
        {
            InitializeComponent();
            Loading();
        }

        public void Loading()
        {
            data = new Database.MysqlDevice();
            List<Device> Devices = data.MysqlDevicesBerles(Globals.User.member_id);
            lvDevices.ItemsSource = Devices;
        }

        private void lvYachts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            Device selectDevice = (Device)(sender as ListView).SelectedItem;
            if (selectDevice != null)
            {
                Globals.selectedDevice = selectDevice;
                ucBerles deviceBerles = new ucBerles();
                //yachtberles.Loading();
                Globals.Main.ccWindow_2.Content = deviceBerles;
            }
        }
    }
}
