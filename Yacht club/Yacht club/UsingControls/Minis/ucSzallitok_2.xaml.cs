﻿using System;
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
using Yacht_club.Database;

namespace Yacht_club.UsingControls.Minis
{
    /// <summary>
    /// Interaction logic for ucSzallitok_2.xaml
    /// </summary>
    public partial class ucSzallitok_2 : UserControl
    {
        MysqlDevice data;
        private List<Device> Devices;
        public ucSzallitok_2()
        {
            InitializeComponent();
            Loading();
        }
        public void Loading()
        {
            data = new MysqlDevice();
            Devices = data.MysqlDevices(Globals.User.member_id);
            Style MessageStackMiniLabel = Application.Current.FindResource("MessageStackMiniLabel") as Style;
            Style ListStackPanel = Application.Current.FindResource("ListStackPanel") as Style;
            for (int i = 0; i < Devices.Count; i++)
            {
                StackPanel panel = new StackPanel();
                panel.Orientation = Orientation.Horizontal;
                panel.Margin = new Thickness(0, 0, 0, 1);
                panel.Height = 29;
                panel.Width = 396.5;
                panel.MouseDown += dpMouse_Click;
                panel.Uid = i.ToString();

                Label nev = new Label();
                nev.Content = "ID: " + Devices[i].id;
                nev.Style = MessageStackMiniLabel;
                nev.Width = 206.5;

                StackPanel panel3 = new StackPanel();
                panel3.Width = 240;
                panel3.Height = 55;

                Label tipus = new Label();
                tipus.Content = "Típus: " + Devices[i].tipus;
                tipus.Style = MessageStackMiniLabel;

                panel.Children.Add(nev);
                panel.Children.Add(tipus);

                panel.Style = ListStackPanel;

                spList.Children.Add(panel);
            }
        }
        private void dpMouse_Click(object sender, MouseButtonEventArgs e)
        {
            Globals.selectedDevice = Devices[int.Parse(((StackPanel)sender).Uid)];
            Globals.Main.ccWindow_Main.Content = new ucSzallito(Globals.selectedDevice.id);
        }
    }
}
