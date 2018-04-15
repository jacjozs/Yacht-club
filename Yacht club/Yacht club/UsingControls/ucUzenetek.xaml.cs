using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Yacht_club.Moduls;

namespace Yacht_club.UsingControls
{
    /// <summary>
    /// Interaction logic for ucUzenetek.xaml
    /// </summary>
    public partial class ucUzenetek : UserControl
    {
        private wMessage MessageDialog;
        Database.MysqlMessage data;
        private List<Message> Messages;
        public ucUzenetek()
        {
            InitializeComponent();
            Loading();
            Globals.UpdateHistory();
        }
        private void Loading()
        {
            data = new Database.MysqlMessage();
            Messages = data.MysqlUserMessages(Globals.User.member_id);
            Style MessageStackLabel = Application.Current.FindResource("MessageStackLabel") as Style;
            Style ListStackPanel = Application.Current.FindResource("ListStackPanel") as Style;
            Style ButtonsStyle = Application.Current.FindResource("Buttons_X") as Style;

            rendezes(Messages);

            for (int i = 0; i < Messages.Count; i++)
            {
                StackPanel panel = new StackPanel();
                panel.Orientation = Orientation.Horizontal;
                panel.Margin = new Thickness(0, 0, 0, 2);
                panel.Height = 48;
                panel.Width = 793;
                panel.MouseDown += new MouseButtonEventHandler(dpMouse_Click);
                panel.Uid = i.ToString();

                Label Info = new Label();
                if (Messages[i].NEWbl)
                {
                    Info.Content = Messages[i].NEWstr;
                    Info.Style = MessageStackLabel;
                }
                if (Messages[i].BlVissza)
                {
                    Info.Content = Messages[i].StrVissza;
                    Info.Style = MessageStackLabel;
                }
                if (!Messages[i].NEWbl && !Messages[i].BlVissza)
                {
                    Info.Content = "Lezárt!";
                    Info.Style = MessageStackLabel;
                }
                Info.Width = 100;

                Label Felado = new Label();
                Felado.Content = "Feladó: " + Messages[i].felado_nev;
                Felado.Style = MessageStackLabel;
                Felado.Width = 267;

                Label Targy = new Label();
                if (Messages[i].device_id != 0)
                    Targy.Content = "Tárgy: Szállitó bérlés!";
                else if (Messages[i].yacht_id != 0)
                    Targy.Content = "Tárgy: Yacht bérlés";
                Targy.Style = MessageStackLabel;
                Targy.Width = 180;

                Label Kelte = new Label();
                Kelte.Content = "Kelt.: " + Messages[i].keletkezett.ToString();
                Kelte.Style = MessageStackLabel;
                Kelte.Width = 200;

                Button X = new Button();
                X.Height = 20;
                X.Width = 20;
                X.Uid = Messages[i].uzenet_id.ToString();
                X.Content = "X";
                X.Style = ButtonsStyle;
                X.Click += new RoutedEventHandler(Delete_Click);

                panel.Children.Add(Info);
                panel.Children.Add(Felado);
                panel.Children.Add(Targy);
                panel.Children.Add(Kelte);
                panel.Children.Add(X);

                panel.Style = ListStackPanel;

                spList.Children.Add(panel);
            }
        }
        private void rendezes(List<Message> list)
        {
            Message seged = new Message();
            for (int i = 0; i < list.Count - 1; i++)
            {
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (!list[i].NEWbl && list[j].NEWbl)
                    {
                        seged = list[j];
                        list[j] = list[i];
                        list[i] = seged;
                    }
                }
            }
        }

        private void dpMouse_Click(object sender, MouseButtonEventArgs e)
        {
            Globals.selectedMessage = Messages[int.Parse(((StackPanel)sender).Uid)];
            data.MysqlMessageNewUpdate(Globals.selectedMessage.uzenet_id);
            Globals.Main.ccWindow_Main.Content = new ucUzenet();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(((Button)sender).Uid);
            MessageDialog = new wMessage(5, id);
            Globals.Main.Opacity = 0.6;
            MessageDialog.ShowDialog();
        }
    }
}
