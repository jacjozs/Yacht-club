﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Yacht_club.UsingControls
{
    /// <summary>
    /// Interaction logic for ucYacht_delete.xaml
    /// </summary>
    public partial class ucYacht_delete : UserControl
    {
        List<Yacht> Yachts;
        private wMessage Message;
        Database.MysqlYacht data;
        public ucYacht_delete()
        {
            InitializeComponent();
            Loading();
            Globals.UpdateHistory();
        }

        public void Loading()
        {
            data = new Database.MysqlYacht();
            Yachts = data.MysqlYachtAll();
            Style MenuStackLabel = Application.Current.FindResource("MenuStackLabel") as Style;
            Style ListStackPanel = Application.Current.FindResource("ListStackPanel") as Style;
            for (int i = 0; i < Yachts.Count; i++)
            {
                StackPanel panel = new StackPanel();
                panel.Orientation = Orientation.Horizontal;
                panel.Margin = new Thickness(0, 0, 0, 2);
                panel.Height = 53;
                panel.Width = 793;
                panel.Uid = i.ToString();
                panel.MouseDown += new MouseButtonEventHandler(dpMouse_Click);

                StackPanel panel2 = new StackPanel();
                panel2.Width = 175;
                panel2.Height = 55;

                Label ID = new Label();
                ID.Content = "ID: " + Yachts[i].id;
                ID.Style = MenuStackLabel;

                Label nev = new Label();
                nev.Content = "Név: " + Yachts[i].nev;
                nev.Style = MenuStackLabel;

                panel2.Children.Add(ID);
                panel2.Children.Add(nev);

                StackPanel panel3 = new StackPanel();
                panel3.Width = 240;
                panel3.Height = 55;

                Label tipus = new Label();
                tipus.Content = "Gyártó: " + Yachts[i].gyarto;
                tipus.Style = MenuStackLabel;

                Label Tulaj = new Label();
                Tulaj.Content = "Tulajdonos: " + Yachts[i].full_name;
                Tulaj.Style = MenuStackLabel;

                panel3.Children.Add(tipus);
                panel3.Children.Add(Tulaj);

                StackPanel panel4 = new StackPanel();
                panel4.Width = 160;
                panel4.Height = 55;

                Label ferohely = new Label();
                ferohely.Content = "Férőhely: " + Yachts[i].ferohely;
                ferohely.Style = MenuStackLabel;

                Label Napiar = new Label();
                Napiar.Content = "Napi ár: " + Yachts[i].napi_ar;
                Napiar.Style = MenuStackLabel;

                panel4.Children.Add(Napiar);
                panel4.Children.Add(ferohely);

                StackPanel panel5 = new StackPanel();
                panel5.Width = 218;
                panel5.Height = 55;
                panel5.HorizontalAlignment = HorizontalAlignment.Left;

                Label kikoto = new Label();
                kikoto.Content = "Kikötő: " + Yachts[i].kikoto;
                kikoto.Style = MenuStackLabel;

                panel5.Children.Add(kikoto);

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
            int id = Yachts[int.Parse(((StackPanel)sender).Uid)].id;
            Message = new wMessage(3, id);
            Globals.Main.Opacity = 0.6;
            Message.ShowDialog();
        }
    }
}
