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
using Yacht_club.Moduls;

namespace Yacht_club.UsingControls.Minis
{
    /// <summary>
    /// Interaction logic for ucUzenetek_2.xaml
    /// </summary>
    public partial class ucUzenetek_2 : UserControl
    {
        Database.MysqlMessage data;
        private List<Message> Messages;
        public ucUzenetek_2()
        {
            InitializeComponent();
            Loading();
        }

        private void Loading()
        {
            data = new Database.MysqlMessage();
            Messages = data.MysqlUserNewMessages();
            Style MessageStackMiniLabel = Application.Current.FindResource("MessageStackMiniLabel") as Style;
            Style ListStackPanel = Application.Current.FindResource("ListStackPanel") as Style;

            for (int i = 0; i < Messages.Count; i++)
            {
                StackPanel panel = new StackPanel();
                panel.Orientation = Orientation.Horizontal;
                panel.Margin = new Thickness(0, 0, 0, 1);
                panel.Height = 29;
                panel.Width = 396.5;
                panel.MouseDown += dpMouse_Click;
                panel.Uid = i.ToString();

                Label Felado = new Label();
                Felado.Content = "Feladó: " + Messages[i].felado_nev;
                Felado.Style = MessageStackMiniLabel;
                Felado.Width = 206.5;

                Label Targy = new Label();
                if (Messages[i].device_id != 0)
                    Targy.Content = "Tárgy: Szállitó bérlés!";
                else if (Messages[i].yacht_id != 0)
                    Targy.Content = "Tárgy: Yacht bérlés";
                Targy.Style = MessageStackMiniLabel;
                Targy.Width = 110;

                Label Vissza = new Label();
                if (Messages[i].BlVissza)
                {
                    Vissza.Content = "Visszajelzés!";
                }
                Vissza.Style = MessageStackMiniLabel;
                Vissza.Width = 70;

                panel.Children.Add(Felado);
                panel.Children.Add(Targy);
                panel.Children.Add(Vissza);

                panel.Style = ListStackPanel;

                spList.Children.Add(panel);
            }

            if (Messages.Count == 0)
            {
                Label nincs = new Label();
                nincs.Content = "Nincs új üzenet";
                nincs.Width = 100;
                nincs.Height = 20;
                nincs.HorizontalContentAlignment = HorizontalAlignment.Center;
                nincs.VerticalContentAlignment = VerticalAlignment.Center;
                nincs.Margin = new Thickness(143, 66, 153, 0);
                nincs.Padding = new Thickness(0);
                nincs.Style = MessageStackMiniLabel;

                spList.Children.Add(nincs);
            }
        }
        private void dpMouse_Click(object sender, MouseButtonEventArgs e)
        {
            Globals.selectedMessage = Messages[int.Parse(((StackPanel)sender).Uid)];
            data.MysqlMessageNewUpdate(Globals.selectedMessage.uzenet_id);
            Globals.Main.ccWindow_Main.Content = new ucUzenet();
        }
    }
}
