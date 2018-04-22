using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows;
using Yacht_club.Moduls;
using System;

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
            data = new Database.MysqlMessage();
            Messages = data.MysqlUserMessages();

            for (int i = 0; i < Messages.Count; i++)
            { // ha a címzettid=jelenlegi user id vel, és el van fogadva és yachtot bérelt
                if (Messages[i].cimzett_id == Globals.User.member_id && Messages[i].elfogadvabl == true && Messages[i].yacht_id >= 0)
                {
                    StackPanel st = new StackPanel();

                    Label Kinek = new Label();
                    Label Mit = new Label();
                    Label Mettol = new Label();
                    Label Meddig = new Label();

                    Kinek.Style = MenuStackLabel;
                    Mit.Style = MenuStackLabel;
                    Mettol.Style = MenuStackLabel;
                    Meddig.Style = MenuStackLabel;

                    Kinek.Content = "Bérlő neve: " + Messages[i].felado_nev + " Jach neve: " + Messages[i].yacht_nev;
                    Mettol.Content = "Mettől-Meddig: " + Messages[i].kezdete.ToShortDateString() + "-" + Messages[i].vege.ToShortDateString();

                    st.Children.Add(Kinek);
                    st.Children.Add(Mettol);
                    st.Children.Add(Meddig);

                    stPanel1.Children.Add(st);
                } // ha a címzettid=jelenlegi user id vel, és el van fogadva és szállítóeszközt bérelt
                if (Messages[i].cimzett_id == Globals.User.member_id && Messages[i].elfogadvabl == true && Messages[i].device_id >= 0)
                {
                    StackPanel st = new StackPanel();

                    Label Kinek = new Label();
                    Label Mit = new Label();
                    Label Mettol = new Label();
                    Label Meddig = new Label();

                    Kinek.Style = MenuStackLabel;
                    Mit.Style = MenuStackLabel;
                    Mettol.Style = MenuStackLabel;
                    Meddig.Style = MenuStackLabel;

                    Kinek.Content = "Bérlő neve: " + Messages[i].felado_nev + " Szállítóeszköz azonosítója: " + Messages[i].device_id;
                    Mettol.Content = "Mettől-Meddig " + Messages[i].kezdete.ToShortDateString() + "-" + Messages[i].vege.ToShortDateString();

                    st.Children.Add(Kinek);
                    st.Children.Add(Mettol);
                    st.Children.Add(Meddig);

                    stPanel2.Children.Add(st);
                }

            }
        }
    }
}
