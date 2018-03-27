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
    /// Interaction logic for ucSzallito_delete.xaml
    /// </summary>
    public partial class ucSzallito_delete : UserControl
    {
        Database.MysqlDevice data;
        public ucSzallito_delete()
        {
            InitializeComponent();
            Loading();
        }

        public void Loading()
        {
            data = new Database.MysqlDevice();
            List<Device> Devices = data.MysqlDeviceAll();
            lvDeviceAll.ItemsSource = Devices;
        }

        private void lvDevicesDelete_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            Device selectDevice = (Device)(sender as ListView).SelectedItem;
            if (selectDevice != null)
            {
                MessageBoxResult delete = MessageBox.Show("Biztos törölni szeretnéd?", "Szállitóeszköz törlés", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (delete == MessageBoxResult.Yes)
                {
                    try
                    {
                        data.MysqlDeleteDevice(selectDevice.id);
                        Loading();
                        Globals.log = "Törlés Sikeres! <Szállitóeszköz>";
                    }
                    catch (Exception)
                    {
                        Globals.log = "Törlés Sikertelen! <Szállitóeszköz>";
                    }
                }
            }
        }
    }
}
