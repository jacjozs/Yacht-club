using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows;
using Yacht_club.Moduls;
using System;
using System.Windows.Input;

namespace Yacht_club.UsingControls
{
    /// <summary>
    /// Interaction logic for ucBerbeadott.xaml
    /// </summary>
    public partial class ucBerbeadott : UserControl
    {
        Database.MysqlMessage data;
        private List<Message> Messages;
        public ucBerbeadott()
        {
            InitializeComponent();
            Globals.UpdateHistory();
            Style MenuStackLabel = Application.Current.FindResource("MenuStackLabel") as Style;
            Style ListStackPanel = Application.Current.FindResource("ListStackPanel") as Style;
            data = new Database.MysqlMessage();
            Messages = data.MysqlUserMessages();

            for (int i = 0; i < Messages.Count; i++)
            { // ha a címzettid=jelenlegi user id vel, és el van fogadva és yachtot bérelt
                if (Messages[i].cimzett_id == Globals.User.member_id && Messages[i].elfogadvabl && Messages[i].yacht_id > 0)
                {
                    StackPanel st = new StackPanel();
                    st.Orientation = Orientation.Horizontal;
                    st.Margin = new Thickness(0, 0, 0, 2);
                    st.Height = 53;
                    st.Background = Globals.MainTheme.hatter;
                    st.Style = ListStackPanel;
                    st.Uid = i.ToString();
                    st.MouseDown += St_MouseDown;

                    StackPanel panel1 = new StackPanel();
                    panel1.Width = 220;
                    StackPanel panel2 = new StackPanel();

                    Label Kinek = new Label();
                    Label Mit = new Label();
                    Label Mettol = new Label();
                    Label Meddig = new Label();

                    Kinek.Style = MenuStackLabel;
                    Mit.Style = MenuStackLabel;
                    Mettol.Style = MenuStackLabel;
                    Meddig.Style = MenuStackLabel;

                    Kinek.Content = "Bérlő neve: " + Messages[i].felado_nev;
                    Kinek.Height = 26.5;
                    Mit.Content = "Yach neve: " + Messages[i].yacht_nev;
                    Mit.Height = 26.5;
                    Mettol.Content = "Mettől: " + Messages[i].kezdete.ToLongDateString();
                    Mettol.Height = 26.5;
                    Meddig.Content = "Meddig: " + Messages[i].vege.ToLongDateString();
                    Meddig.Height = 26.5;

                    panel1.Children.Add(Kinek);
                    panel1.Children.Add(Mit);
                    panel2.Children.Add(Mettol);
                    panel2.Children.Add(Meddig);

                    st.Children.Add(panel1);
                    st.Children.Add(panel2);

                    stPanel1.Children.Add(st);
                } // ha a címzettid=jelenlegi user id vel, és el van fogadva és szállítóeszközt bérelt
                if (Messages[i].cimzett_id == Globals.User.member_id && Messages[i].elfogadvabl == true && Messages[i].device_id > 0)
                {
                    StackPanel st = new StackPanel();
                    st.Orientation = Orientation.Horizontal;
                    st.Margin = new Thickness(0, 0, 0, 2);
                    st.Height = 53;
                    st.Background = Globals.MainTheme.hatter;
                    st.Style = ListStackPanel;
                    st.Uid = i.ToString();
                    st.MouseDown += St_MouseDown;

                    StackPanel panel1 = new StackPanel();
                    panel1.Width = 220;
                    StackPanel panel2 = new StackPanel();

                    Label Kinek = new Label();
                    Label Mit = new Label();
                    Label Mettol = new Label();
                    Label Meddig = new Label();

                    Kinek.Style = MenuStackLabel;
                    Mit.Style = MenuStackLabel;
                    Mettol.Style = MenuStackLabel;
                    Meddig.Style = MenuStackLabel;

                    Kinek.Content = "Bérlő neve: " + Messages[i].felado_nev;
                    Kinek.Height = 26.5;
                    Mit.Content = "Szállitó ID: " + Messages[i].device_id;
                    Mit.Height = 26.5;
                    Mettol.Content = "Mettől: " + Messages[i].kezdete.ToLongDateString();
                    Mettol.Height = 26.5;
                    Meddig.Content = "Meddig: " + Messages[i].vege.ToLongDateString();
                    Meddig.Height = 26.5;

                    panel1.Children.Add(Kinek);
                    panel1.Children.Add(Mit);
                    panel2.Children.Add(Mettol);
                    panel2.Children.Add(Meddig);

                    st.Children.Add(panel1);
                    st.Children.Add(panel2);

                    stPanel2.Children.Add(st);
                }
            }
        }

        private void St_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Globals.selectedMessage = Messages[int.Parse(((StackPanel)sender).Uid)];
            Globals.Main.ccWindow_Main.Content = new ucUzenet();
        }
    }
}
