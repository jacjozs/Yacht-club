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
    /// Interaction logic for ucSzallitok.xaml
    /// </summary>
    public partial class ucSzallitok : UserControl
    {
        Database.MysqlDevice data;
        private List<Device> Devices;
        public ucSzallitok()
        {
            InitializeComponent();
            Loading();
        }

        private void Loading()
        {
            data = new Database.MysqlDevice();
            Devices = data.MysqlDevices(Globals.User.member_id);
            Style MenuStackLabel = Application.Current.FindResource("MenuStackLabel") as Style;
            Style ListStackPanel = Application.Current.FindResource("ListStackPanel") as Style;
            for (int i = 0; i < Devices.Count; i++)
            {
                StackPanel panel = new StackPanel();
                panel.Orientation = Orientation.Horizontal;
                panel.Height = 45;
                panel.Width = 533;
                panel.MouseDown += new MouseButtonEventHandler(dpMouse_Click);
                panel.Name = "D" + i.ToString();

                StackPanel panel2 = new StackPanel();
                panel2.Width = 110;
                panel2.Height = 40;

                Label ID = new Label();
                ID.Content = "ID: " + Devices[i].id;
                ID.Style = MenuStackLabel;

                Label Tipus = new Label();
                Tipus.Content = "Típus: " + Devices[i].tipus;
                Tipus.Style = MenuStackLabel;

                panel2.Children.Add(ID);
                panel2.Children.Add(Tipus);

                StackPanel panel3 = new StackPanel();
                panel3.Width = 150;
                panel3.Height = 40;

                Label Napiar = new Label();
                Napiar.Content = "Napi ár: " + Devices[i].napi_ar;
                Napiar.Style = MenuStackLabel;

                Label Tulaj = new Label();
                Tulaj.Content = "Tulajdonos: " + Devices[i].full_name;
                Tulaj.Style = MenuStackLabel;

                panel3.Children.Add(Napiar);
                panel3.Children.Add(Tulaj);

                StackPanel panel4 = new StackPanel();
                panel4.Width = 150;
                panel4.Height = 40;

                Label Teherbiras = new Label();
                Teherbiras.Content = "Teherbirás: " + Devices[i].max_suly;
                Teherbiras.Style = MenuStackLabel;

                Label Magas = new Label();
                Magas.Content = "Magas: " + Devices[i].max_magas;
                Magas.Style = MenuStackLabel;

                panel4.Children.Add(Teherbiras);
                panel4.Children.Add(Magas);

                StackPanel panel5 = new StackPanel();
                panel5.Width = 150;
                panel5.Height = 40;
                panel5.HorizontalAlignment = HorizontalAlignment.Left;

                Label Szeles = new Label();
                Szeles.Content = "Széles: " + Devices[i].max_szeles;
                Szeles.Style = MenuStackLabel;

                Label Hosszu = new Label();
                Hosszu.Content = "Hosszú: " + Devices[i].max_hossz;
                Hosszu.Style = MenuStackLabel;

                panel5.Children.Add(Szeles);
                panel5.Children.Add(Hosszu);

                panel.Children.Add(panel2);
                panel.Children.Add(panel3);
                panel.Children.Add(panel4);
                panel.Children.Add(panel5);

                panel.Style = ListStackPanel;

                spList.Children.Add(panel);
            }
        }
        private void dpMouse_Click(object sender, MouseButtonEventArgs e)
        {
            StackPanel device = (StackPanel)sender;
            Globals.selectedDevice = Devices[int.Parse(device.Name.Substring(1))];
            Globals.Main.ccWindow_2.Content = new ucSzallito();
        }
    }
}
