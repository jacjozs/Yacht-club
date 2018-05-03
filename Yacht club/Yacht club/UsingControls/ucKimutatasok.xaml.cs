using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Collections.Generic;
using Yacht_club.Moduls;
using Yacht_club.Database;
using System.Linq;
using System;
using System.Windows.Input;

namespace Yacht_club.UsingControls
{
    /// <summary>
    /// Interaction logic for ucKimutatasok.xaml
    /// </summary>
    public partial class ucKimutatasok : UserControl
    {
        MysqlMessage data;
        private List<Message> Messages;
        private Diagram diagram;
        private bool bevet = false;
        private int honap;

        public ucKimutatasok()
        {
            InitializeComponent();
            Loading();
        }
        private void Loading()
        {
            data = new MysqlMessage();
            if(!Globals.User.login.admin)
                Messages = data.MysqlUserMessages();
            else
                Messages = data.MysqlAllMessages();
            diagram = new Diagram();
            diagram.yachts = new List<List<int>>();
            diagram.devices = new List<int>();
            Diagrams(0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);
            GridCursor.Margin = new Thickness(10 + (135.5 * index), 45, 0, 329);
            Diagrams(index);
        }

        private void Diagrams(int index)
        {
            switch (index)
            {
                case 0://Idei bevétel
                    honap = 0;
                    diagram.oszlopC = 12;
                    diagram.oszlopX = (813 / 12) - 10;
                    diagram.oszlopY = new double[12];
                    diagram.sorN = new string[12] { "Január", "Február", "Március", "Április", "Május", "Június", "Július", "Agusztus", "Szeptem.", "Oktober", "Novem.", "Decem." };
                    for (int i = 0; i < 12; i++)
                    {
                        months months = (months)i;
                        string month = months.ToString().ToUpper();
                        diagram.oszlopY[i] = Messages.Where(x => string.Format("{0:yyyy,MMMM}", x.keletkezett).ToUpper() == (string.Format("{0:yyyy}", DateTime.Now)) + "," + month).Select(x => x.price).Sum();
                    }
                    bevetel(false);
                    break;
                case 1://Idei bérlések
                    honap = 0;
                    diagram.oszlopC = 12;
                    diagram.oszlopX = (813 / 12) - 10;
                    diagram.oszlopY = new double[12];
                    diagram.sorN = new string[12] { "Január", "Február", "Március", "Április", "Május", "Június", "Július", "Agusztus", "Szeptem.", "Oktober", "Novem.", "Decem." };
                    diagram.yachts.Clear();
                    diagram.devices.Clear();
                    for (int i = 0; i < 12; i++)
                    {
                        months months = (months)i;
                        string month = months.ToString().ToUpper();
                        diagram.oszlopY[i] = Messages.Count(x => string.Format("{0:yyyy,MMMM}", x.keletkezett).ToUpper() == (string.Format("{0:yyyy}", DateTime.Now)) + "," + month);
                        diagram.yachts.Add(Messages.Where(x => string.Format("{0:yyyy,MMMM}", x.keletkezett).ToUpper() == (string.Format("{0:yyyy}", DateTime.Now)) + "," + month).Select(x => x.yacht_id).ToList());
                    }
                    berles(false);
                    break;
                case 2://Havi bevétel
                    diagram.oszlopC = 0;
                    diagram.yachts.Clear();
                    diagram.devices.Clear();
                    bevet = true;
                    bevetel(true);
                    break;
                case 3://Havi bérlések
                    diagram.oszlopC = 0;
                    diagram.yachts.Clear();
                    diagram.devices.Clear();
                    bevet = false;
                    berles(true);
                    break;
                case 4://Heti bevétel
                    honap = 0;
                    diagram.oszlopC = 7;
                    diagram.oszlopX = (813 / 7) - 10;
                    diagram.oszlopY = new double[7];
                    diagram.sorN = new string[7];
                    for (int i = 0; i < 7; i++)
                    {
                        days days = (days)i;
                        diagram.sorN[i] = days.ToString();
                        string day = diagram.sorN[i].ToUpper();
                        diagram.oszlopY[i] = Messages.Where(x => string.Format("{0:yyyy,MMMM,dddd}", x.keletkezett).ToUpper() == (string.Format("{0:yyyy,MMMM}", DateTime.Now).ToUpper()) + "," + day).Select(x => x.price).Sum();
                    }
                    bevetel(false);
                    break;
                case 5://Heti bérlések
                    honap = 0;
                    diagram.oszlopC = 7;
                    diagram.oszlopX = (813 / 7) - 10;
                    diagram.oszlopY = new double[7];
                    diagram.sorN = new string[7];
                    diagram.oszlopY = new double[7];
                    diagram.yachts.Clear();
                    diagram.devices.Clear();
                    for (int i = 0; i < 7; i++)
                    {
                        days days = (days)i;
                        diagram.sorN[i] = days.ToString();
                        string day = diagram.sorN[i].ToUpper();
                        diagram.oszlopY[i] = Messages.Count(x => string.Format("{0:yyyy,MMMM,dddd}", x.keletkezett).ToUpper() == (string.Format("{0:yyyy,MMMM}", DateTime.Now).ToUpper()) + "," + day);
                        diagram.yachts.Add(Messages.Where(x => string.Format("{0:yyyy,MMMM,dddd}", x.keletkezett).ToUpper() == (string.Format("{0:yyyy,MMMM}", DateTime.Now).ToUpper()) + "," + day).Select(x => x.yacht_id).ToList());
                    }
                    berles(false);
                    break;
            }
        }

        private void bevetel(bool tabla)
        {
            stDiagram.Children.Clear();
            stNames.Children.Clear();
            if (tabla) Tablazat();
            else
            {
                if (diagram.oszlopC == 0) diagram.oszlopC = honap;
                for (int i = 0; i < diagram.oszlopC; i++)
                {
                    Rectangle oszlop = new Rectangle();
                    oszlop.Width = diagram.oszlopX;
                    if (oszlop.Width < 25) oszlop.Width = 25;
                    try
                    {
                        oszlop.Height = diagram.oszlopY[i] * (270 / diagram.oszlopY.Max());
                    }
                    catch (Exception)
                    {
                        oszlop.Height = 0;
                    }
                    oszlop.Fill = Globals.MainTheme.hatter;
                    oszlop.Margin = new Thickness(10, 0, 0, 0);
                    if (honap == 28) oszlop.Margin = new Thickness(4, 0, 0, 0);
                    if (honap == 30) oszlop.Margin = new Thickness(2, 0, 0, 0);
                    if (honap == 31) oszlop.Margin = new Thickness(1, 0, 0, 0);
                    oszlop.VerticalAlignment = VerticalAlignment.Bottom;
                    stDiagram.Children.Add(oszlop);
                    Label name = new Label();
                    name.Content = diagram.sorN[i];
                    name.Width = oszlop.Width;
                    name.Margin = new Thickness(10, 0, 0, 0);
                    if (honap == 28) name.Margin = new Thickness(4, 0, 0, 0);
                    if (honap == 30) name.Margin = new Thickness(2, 0, 0, 0);
                    if (honap == 31) name.Margin = new Thickness(1, 0, 0, 0);
                    name.HorizontalContentAlignment = HorizontalAlignment.Center;
                    name.Foreground = Globals.MainTheme.betu_szin;
                    name.VerticalAlignment = VerticalAlignment.Center;
                    stNames.Children.Add(name);
                }
            }
        }

        private void berles(bool tabla)
        {
            Style DiagramLabel = Application.Current.FindResource("DiagramLabel") as Style;
            stDiagram.Children.Clear();
            stNames.Children.Clear();
            if (tabla) Tablazat();
            else
            {
                if (diagram.oszlopC == 0) diagram.oszlopC = honap;
                for (int i = 0; i < diagram.oszlopC; i++)
                {
                    StackPanel oszlop = new StackPanel();
                    oszlop.Width = diagram.oszlopX;
                    if (oszlop.Width < 25) oszlop.Width = 25;
                    try
                    {
                        oszlop.Height = diagram.oszlopY[i] * (270 / diagram.oszlopY.Max());
                    }
                    catch (Exception)
                    {
                        oszlop.Height = 0;
                    }
                    oszlop.Background = Globals.MainTheme.hatter;
                    oszlop.Margin = new Thickness(10, 0, 0, 0);
                    if (honap == 28) oszlop.Margin = new Thickness(4, 0, 0, 0);
                    if (honap == 30) oszlop.Margin = new Thickness(2, 0, 0, 0);
                    if (honap == 31) oszlop.Margin = new Thickness(1, 0, 0, 0);
                    oszlop.VerticalAlignment = VerticalAlignment.Bottom;
                    StackPanel list = new StackPanel();
                    list.Background = null;
                    list.VerticalAlignment = VerticalAlignment.Top;
                    try
                    {
                        for (int j = 0; j < diagram.yachts[i].Count; j++)
                        {
                            if (diagram.yachts[i][j] != 0)
                            {
                                Label devyac = new Label();
                                devyac.Style = DiagramLabel;
                                devyac.Height = oszlop.Height / diagram.yachts[i].Where(x => x != 0).Count() - 10;
                                if (devyac.Height <= 20)
                                    devyac.Height = 25;
                                devyac.Background = Globals.MainTheme.uc_hatter;
                                if (oszlop.Width < 25)
                                {
                                    devyac.Margin = new Thickness(0);
                                    devyac.FontSize = 10;
                                }
                                else devyac.Margin = new Thickness(5, 2.5, 5, 0);
                                devyac.HorizontalContentAlignment = HorizontalAlignment.Center;
                                devyac.VerticalContentAlignment = VerticalAlignment.Center;
                                if (diagram.yachts[i].Count != 0)
                                    devyac.Content = diagram.yachts[i][j].ToString();
                                list.Children.Add(devyac);
                            }
                        }
                        if (diagram.oszlopY[i] == 0)
                            list.Height = 0;
                        else
                            list.Height = (oszlop.Height * diagram.yachts[i].Count) - 10;
                        if (list.Height <= 20)
                            list.Height = 25;
                    }
                    catch (Exception)
                    { }
                    oszlop.Children.Add(list);
                    stDiagram.Children.Add(oszlop);
                    Label name = new Label();
                    name.Content = diagram.sorN[i];
                    name.Width = oszlop.Width;
                    if (oszlop.Width < 25) name.FontSize = 10;
                    name.Margin = new Thickness(10, 0, 0, 0);
                    if (honap == 28) name.Margin = new Thickness(4, 0, 0, 0);
                    if (honap == 30) name.Margin = new Thickness(2, 0, 0, 0);
                    if (honap == 31) name.Margin = new Thickness(1, 0, 0, 0);
                    name.HorizontalContentAlignment = HorizontalAlignment.Center;
                    name.Foreground = Globals.MainTheme.betu_szin;
                    name.VerticalAlignment = VerticalAlignment.Center;
                    stNames.Children.Add(name);
                }
            }
        }

        private void Tablazat()
        {
            Style Buttons = Application.Current.FindResource("Buttons") as Style;
            StackPanel elso = new StackPanel();
            StackPanel masodik = new StackPanel();
            StackPanel harmadik = new StackPanel();
            StackPanel negyedik = new StackPanel();
            elso.Height = 200;
            elso.Width = 203.75;
            Button gomb = new Button() { Uid = "0", Content = "Január", Height = 50, Width = 175, Margin = new Thickness(0, 10, 10, 0), Style = Buttons, FontWeight = FontWeights.Bold, Background = Globals.MainTheme.hatter, Foreground = Globals.MainTheme.betu_szin, VerticalContentAlignment = VerticalAlignment.Center, HorizontalContentAlignment = HorizontalAlignment.Center };
            gomb.Click += UcKimutatasok_Click;
            elso.Children.Add(gomb);
            Button gomb1 = new Button() { Uid = "4", Content = "Május", Height = 50, Width = 175, Margin = new Thickness(0, 10, 10, 0), Style = Buttons, FontWeight = FontWeights.Bold, Background = Globals.MainTheme.hatter, Foreground = Globals.MainTheme.betu_szin, VerticalContentAlignment = VerticalAlignment.Center, HorizontalContentAlignment = HorizontalAlignment.Center };
            gomb1.Click += UcKimutatasok_Click;
            elso.Children.Add(gomb1);
            Button gomb2 = new Button() { Uid = "8", Content = "Szeptember", Height = 50, Width = 175, Margin = new Thickness(0, 10, 10, 0), Style = Buttons, FontWeight = FontWeights.Bold, Background = Globals.MainTheme.hatter, Foreground = Globals.MainTheme.betu_szin, VerticalContentAlignment = VerticalAlignment.Center, HorizontalContentAlignment = HorizontalAlignment.Center };
            gomb2.Click += UcKimutatasok_Click;
            elso.Children.Add(gomb2);
            masodik.Height = 200;
            masodik.Width = 203.75;
            Button gomb3 = new Button() { Uid = "1", Content = "Február", Height = 50, Width = 175, Margin = new Thickness(0, 10, 10, 0), Style = Buttons, FontWeight = FontWeights.Bold, Background = Globals.MainTheme.hatter, Foreground = Globals.MainTheme.betu_szin, VerticalContentAlignment = VerticalAlignment.Center, HorizontalContentAlignment = HorizontalAlignment.Center };
            gomb3.Click += UcKimutatasok_Click;
            masodik.Children.Add(gomb3);
            Button gomb4 = new Button() { Uid = "5", Content = "Június", Height = 50, Width = 175, Margin = new Thickness(0, 10, 10, 0), Style = Buttons, FontWeight = FontWeights.Bold, Background = Globals.MainTheme.hatter, Foreground = Globals.MainTheme.betu_szin, VerticalContentAlignment = VerticalAlignment.Center, HorizontalContentAlignment = HorizontalAlignment.Center };
            gomb4.Click += UcKimutatasok_Click;
            masodik.Children.Add(gomb4);
            Button gomb5 = new Button() { Uid = "9", Content = "Oktober", Height = 50, Width = 175, Margin = new Thickness(0, 10, 10, 0), Style = Buttons, FontWeight = FontWeights.Bold, Background = Globals.MainTheme.hatter, Foreground = Globals.MainTheme.betu_szin, VerticalContentAlignment = VerticalAlignment.Center, HorizontalContentAlignment = HorizontalAlignment.Center };
            gomb5.Click += UcKimutatasok_Click;
            masodik.Children.Add(gomb5);
            harmadik.Height = 200;
            harmadik.Width = 203.75;
            Button gomb6 = new Button() { Uid = "2", Content = "Március", Height = 50, Width = 175, Margin = new Thickness(0, 10, 10, 0), Style = Buttons, FontWeight = FontWeights.Bold, Background = Globals.MainTheme.hatter, Foreground = Globals.MainTheme.betu_szin, VerticalContentAlignment = VerticalAlignment.Center, HorizontalContentAlignment = HorizontalAlignment.Center };
            gomb6.Click += UcKimutatasok_Click;
            harmadik.Children.Add(gomb6);
            Button gomb7 = new Button() { Uid = "6", Content = "Július", Height = 50, Width = 175, Margin = new Thickness(0, 10, 10, 0), Style = Buttons, FontWeight = FontWeights.Bold, Background = Globals.MainTheme.hatter, Foreground = Globals.MainTheme.betu_szin, VerticalContentAlignment = VerticalAlignment.Center, HorizontalContentAlignment = HorizontalAlignment.Center };
            gomb7.Click += UcKimutatasok_Click;
            harmadik.Children.Add(gomb7);
            Button gomb8 = new Button() { Uid = "10", Content = "November", Height = 50, Width = 175, Margin = new Thickness(0, 10, 10, 0), Style = Buttons, FontWeight = FontWeights.Bold, Background = Globals.MainTheme.hatter, Foreground = Globals.MainTheme.betu_szin, VerticalContentAlignment = VerticalAlignment.Center, HorizontalContentAlignment = HorizontalAlignment.Center };
            gomb8.Click += UcKimutatasok_Click;
            harmadik.Children.Add(gomb8);
            negyedik.Height = 200;
            negyedik.Width = 203.75;
            Button gomb9 = new Button() { Uid = "3", Content = "Április", Height = 50, Width = 175, Margin = new Thickness(0, 10, 10, 0), Style = Buttons, FontWeight = FontWeights.Bold, Background = Globals.MainTheme.hatter, Foreground = Globals.MainTheme.betu_szin, VerticalContentAlignment = VerticalAlignment.Center, HorizontalContentAlignment = HorizontalAlignment.Center };
            gomb9.Click += UcKimutatasok_Click;
            negyedik.Children.Add(gomb9);
            Button gomb10 = new Button() { Uid = "7", Content = "Agusztus", Height = 50, Width = 175, Margin = new Thickness(0, 10, 10, 0), Style = Buttons, FontWeight = FontWeights.Bold, Background = Globals.MainTheme.hatter, Foreground = Globals.MainTheme.betu_szin, VerticalContentAlignment = VerticalAlignment.Center, HorizontalContentAlignment = HorizontalAlignment.Center };
            gomb10.Click += UcKimutatasok_Click;
            negyedik.Children.Add(gomb10);
            Button gomb11 = new Button() { Uid = "11", Content = "December", Height = 50, Width = 175, Margin = new Thickness(0, 10, 10, 0), Style = Buttons, FontWeight = FontWeights.Bold, Background = Globals.MainTheme.hatter, Foreground = Globals.MainTheme.betu_szin, VerticalContentAlignment = VerticalAlignment.Center, HorizontalContentAlignment = HorizontalAlignment.Center };
            gomb11.Click += UcKimutatasok_Click;
            negyedik.Children.Add(gomb11);
            stDiagram.Children.Add(elso);
            stDiagram.Children.Add(masodik);
            stDiagram.Children.Add(harmadik);
            stDiagram.Children.Add(negyedik);
        }

        private void UcKimutatasok_Click(object sender, RoutedEventArgs e)
        {
            switch (int.Parse(((Button)sender).Uid))
            {
                case 0:
                case 2:
                case 4:
                case 6:
                case 7:
                case 9:
                case 11:
                    honap = 31;
                    break;
                case 1:
                    honap = 28;
                    break;
                default:
                    honap = 30;
                    break;
            }
            diagram.sorN = new string[honap];
            diagram.oszlopX = (813 / honap) - 10;
            diagram.oszlopY = new double[honap];
            for (int i = 0; i < honap; i++)
            {
                diagram.sorN[i] = (i + 1).ToString();
                months months = (months)int.Parse(((Button)sender).Uid);
                string month = months.ToString().ToUpper();
                diagram.oszlopY[i] = Messages.Count(x => string.Format("{0:MMMM,d}", x.keletkezett).ToUpper() == month + "," + diagram.sorN[i]);
            }
            if (bevet)
                bevetel(false);
            else
                berles(false);
        }

        private struct Diagram
        {
            public int oszlopC;
            public double oszlopX;
            public double[] oszlopY;
            public string[] sorN;
            public List<List<int>> yachts;
            public List<int> devices;
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

        private enum months
        {
            Január,
            Február,
            Március,
            Április,
            Május,
            Június,
            Július,
            Agusztus,
            Szeptember,
            Oktober,
            November,
            December
        }
    }
}
