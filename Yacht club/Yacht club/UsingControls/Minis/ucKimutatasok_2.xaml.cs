using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Yacht_club.Database;
using Yacht_club.Moduls;

namespace Yacht_club.UsingControls.Minis
{
    /// <summary>
    /// Interaction logic for ucKimutatasok_2.xaml
    /// </summary>
    public partial class ucKimutatasok_2 : UserControl
    {
        MysqlMessage data;
        private List<Message> Messages;
        private Diagram diagram;

        public ucKimutatasok_2()
        {
            InitializeComponent();
            Loading();
        }
        private void Loading()
        {
            data = new MysqlMessage();
            if (!Globals.User.login.admin)
                Messages = data.MysqlUserMessages();
            else
                Messages = data.MysqlAllMessages();
            diagram = new Diagram();
            diagram.yachts = new List<List<int>>();
            diagram.devices = new List<List<int>>();
            diagram.oszlopC = 7;
            diagram.oszlopX = (416.5 / 7) - 7;
            diagram.oszlopY = new double[7];
            diagram.sorN = new string[7];
            diagram.yachts.Clear();
            diagram.devices.Clear();
            for (int i = 0; i < 7; i++)
            {
                days days = (days)i;
                diagram.sorN[i] = days.ToString();
                string day = diagram.sorN[i].ToUpper();
                if(Globals.User.login.admin)
                {
                    diagram.oszlopY[i] = Messages.Count(x => string.Format("{0:yyyy,MMMM,dddd}", x.keletkezett).ToUpper() == (string.Format("{0:yyyy,MMMM}", DateTime.Now).ToUpper()) + "," +
                    day && x.elfogadvabl);
                    diagram.yachts.Add(Messages.Where(x => string.Format("{0:yyyy,MMMM,dddd}", x.keletkezett).ToUpper() == (string.Format("{0:yyyy,MMMM}", DateTime.Now).ToUpper()) + "," +
                    day && x.yacht_id != 0 && x.elfogadvabl).Select(x => x.yacht_id).ToList());
                    diagram.devices.Add(Messages.Where(x => string.Format("{0:yyyy,MMMM,dddd}", x.keletkezett).ToUpper() == (string.Format("{0:yyyy,MMMM}", DateTime.Now).ToUpper()) + "," +
                    day && x.device_id != 0 && x.elfogadvabl).Select(x => x.device_id).ToList());
                }
                else
                {
                    diagram.oszlopY[i] = Messages.Count(x => string.Format("{0:yyyy,MMMM,dddd}", x.keletkezett).ToUpper() == (string.Format("{0:yyyy,MMMM}", DateTime.Now).ToUpper()) + "," +
                    day && x.elfogadvabl && x.cimzett_id == Globals.User.member_id);
                    diagram.yachts.Add(Messages.Where(x => string.Format("{0:yyyy,MMMM,dddd}", x.keletkezett).ToUpper() == (string.Format("{0:yyyy,MMMM}", DateTime.Now).ToUpper()) + "," +
                    day && x.yacht_id != 0 && x.elfogadvabl && x.cimzett_id == Globals.User.member_id).Select(x => x.yacht_id).ToList());
                    diagram.devices.Add(Messages.Where(x => string.Format("{0:yyyy,MMMM,dddd}", x.keletkezett).ToUpper() == (string.Format("{0:yyyy,MMMM}", DateTime.Now).ToUpper()) + "," +
                    day && x.device_id != 0 && x.elfogadvabl && x.cimzett_id == Globals.User.member_id).Select(x => x.device_id).ToList());
                }
            }
            berles();
        }

        private void berles()
        {
            Style DiagramLabel = Application.Current.FindResource("DiagramLabel") as Style;
            for (int i = 0; i < diagram.oszlopC; i++)
            {
                StackPanel oszlop = new StackPanel();
                oszlop.Width = diagram.oszlopX;
                oszlop.Height = diagram.oszlopY[i] * (157 / diagram.oszlopY.Max());
                if (oszlop.Height != 0 && !(oszlop.Height > 0) || oszlop.Height == 0)
                    oszlop.Height = 5;
                oszlop.Background = Globals.MainTheme.hatter;
                oszlop.Margin = new Thickness(5, 0, 0, 0);
                oszlop.VerticalAlignment = VerticalAlignment.Bottom;
                StackPanel list = new StackPanel();
                list.Background = null;
                list.VerticalAlignment = VerticalAlignment.Top;
                string devOrYac;
                try
                {
                    for (int j = 0; j < diagram.yachts[i].Count; j++)
                    {
                        devOrYac = "Yach.";
                        if (diagram.yachts[i][j] != 0)
                        {
                            Label devyac = new Label();
                            devyac.Style = DiagramLabel;
                            if (devOrYac == "Yach.")
                                devyac.Uid = diagram.yachts[i][j].ToString() + ":" + devOrYac;
                            devyac.Height = oszlop.Height / diagram.oszlopY[i] - 5;
                            devyac.Background = Globals.MainTheme.uc_hatter;
                            devyac.FontSize = 10;
                            devyac.Margin = new Thickness(2.5, 2.5, 2.5, 0);
                            devyac.Foreground = Globals.MainTheme.betu_szin;
                            devyac.HorizontalContentAlignment = HorizontalAlignment.Center;
                            devyac.VerticalContentAlignment = VerticalAlignment.Center;
                            if (diagram.yachts[i].Count != 0 && diagram.oszlopC <= 12)
                                devyac.Content = diagram.yachts[i][j].ToString() + ":" + devOrYac;
                            else
                                devyac.Content = diagram.yachts[i][j].ToString();
                            devyac.MouseLeftButtonDown += Label_MouseLeftButtonDown;
                            if (devyac.Content != null)
                                list.Children.Add(devyac);
                        }
                    }
                    for (int j = 0; j < diagram.devices[i].Count; j++)
                    {
                        devOrYac = "Száll.";
                        if (diagram.devices[i][j] != 0)
                        {
                            Label devyac = new Label();
                            devyac.Style = DiagramLabel;
                            if (devOrYac == "Száll.")
                                devyac.Uid = diagram.devices[i][j].ToString() + ":" + devOrYac;
                            devyac.Height = oszlop.Height / diagram.oszlopY[i] - 5;
                            devyac.Background = Globals.MainTheme.uc_hatter;
                            devyac.FontSize = 10;
                            devyac.Margin = new Thickness(2.5, 2.5, 2.5, 0);
                            devyac.Foreground = Globals.MainTheme.betu_szin;
                            devyac.HorizontalContentAlignment = HorizontalAlignment.Center;
                            devyac.VerticalContentAlignment = VerticalAlignment.Center;
                            if (diagram.devices[i].Count != 0 && diagram.oszlopC <= 12)
                                devyac.Content = diagram.devices[i][j].ToString() + ":" + devOrYac;
                            else
                                devyac.Content = diagram.devices[i][j].ToString();
                            devyac.MouseLeftButtonDown += Label_MouseLeftButtonDown;
                            if (devyac.Content != null)
                                list.Children.Add(devyac);
                        }
                    }
                }
                catch (Exception)
                { }
                oszlop.Children.Add(list);
                stDiagram.Children.Add(oszlop);
                Label name = new Label();
                name.Content = diagram.sorN[i];
                name.Width = oszlop.Width;
                name.FontSize = 10;
                name.Margin = new Thickness(5 , 0, 0, 0);
                name.HorizontalContentAlignment = HorizontalAlignment.Center;
                name.Foreground = Globals.MainTheme.betu_szin;
                name.VerticalAlignment = VerticalAlignment.Center;
                stNames.Children.Add(name);
            }
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            int id = int.Parse(((Label)sender).Uid.Split(':')[0]);
            string name = ((Label)sender).Uid.Split(':')[1];
            switch (name)
            {
                case "Yach.":
                    MysqlYacht yachtDat = new MysqlYacht();
                    Globals.selectedYacht = yachtDat.MysqlYachtSelect(id);
                    Globals.Main.ccWindow_Main.Content = new ucYacht(id);
                    yachtDat = null;
                    break;
                case "Száll.":
                    MysqlDevice deviceDat = new MysqlDevice();
                    Globals.selectedDevice = deviceDat.MysqlDeviceSelect(id);
                    Globals.Main.ccWindow_Main.Content = new ucSzallito(id);
                    deviceDat = null;
                    break;
            }
        }

        private struct Diagram
        {
            public int oszlopC;
            public double oszlopX;
            public double[] oszlopY;
            public string[] sorN;
            public List<List<int>> yachts;
            public List<List<int>> devices;
        }

        private enum days
        {
            Hétfő,
            Kedd,
            Szerda,
            Csütörtök,
            Péntek,
            Szombat,
            Vasárnap
        }
    }
}
