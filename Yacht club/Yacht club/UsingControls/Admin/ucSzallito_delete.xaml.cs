using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Yacht_club.UsingControls
{
    /// <summary>
    /// Interaction logic for ucSzallito_delete.xaml
    /// </summary>
    public partial class ucSzallito_delete : UserControl
    {
        private wMessage Message;
        Database.MysqlDevice data;
        private List<Device> Devices;
        public ucSzallito_delete()
        {
            InitializeComponent();
            Loading();
            Globals.UpdateHistory();
        }

        public void Loading()
        {
            data = new Database.MysqlDevice();
            Devices = data.MysqlDeviceAll();
            Style MenuStackLabel = Application.Current.FindResource("MenuStackLabel") as Style;
            Style ListStackPanel = Application.Current.FindResource("ListStackPanel") as Style;
            for (int i = 0; i < Devices.Count; i++)
            {
                StackPanel panel = new StackPanel();
                panel.Orientation = Orientation.Horizontal;
                panel.Margin = new Thickness(0, 0, 0, 2);
                panel.Height = 53;
                panel.Width = 793;
                panel.Uid = i.ToString();
                panel.MouseDown += new MouseButtonEventHandler(dpMouse_Click);

                StackPanel panel2 = new StackPanel();
                panel2.Width = 165;
                panel2.Height = 55;

                Label ID = new Label();
                ID.Content = "ID: " + Devices[i].id;
                ID.Style = MenuStackLabel;

                Label Tipus = new Label();
                Tipus.Content = "Típus: " + Devices[i].tipus;
                Tipus.Style = MenuStackLabel;

                panel2.Children.Add(ID);
                panel2.Children.Add(Tipus);

                StackPanel panel3 = new StackPanel();
                panel3.Width = 240;
                panel3.Height = 55;

                Label Napiar = new Label();
                Napiar.Content = "Napi ár: " + Devices[i].napi_ar;
                Napiar.Style = MenuStackLabel;

                Label Tulaj = new Label();
                Tulaj.Content = "Tulajdonos: " + Devices[i].full_name;
                Tulaj.Style = MenuStackLabel;

                panel3.Children.Add(Napiar);
                panel3.Children.Add(Tulaj);

                StackPanel panel4 = new StackPanel();
                panel4.Width = 194;
                panel4.Height = 55;

                Label Teherbiras = new Label();
                Teherbiras.Content = "Max teherbirás(t): " + Devices[i].max_suly;
                Teherbiras.Style = MenuStackLabel;

                panel4.Children.Add(Teherbiras);

                StackPanel panel5 = new StackPanel();
                panel5.Width = 194;
                panel5.Height = 55;
                panel5.HorizontalAlignment = HorizontalAlignment.Left;

                Label Hosszu = new Label();
                Hosszu.Content = "Max hosszúság(m): " + Devices[i].max_hossz;
                Hosszu.Style = MenuStackLabel;

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

            int id = Devices[int.Parse(((StackPanel)sender).Uid)].id;
            Message = new wMessage(4, id);
            Globals.Main.Opacity = 0.6;
            Message.ShowDialog();
        }
    }
}
