using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using Yacht_club.Moduls;
using Yacht_club.Database;
using System.Linq;
using System;
using System.Windows.Input;
using System.Windows.Media;

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
        private int index;

        public ucKimutatasok()
        {
            InitializeComponent();
            Loading();
        }
        private void Loading()
        {
            data = new MysqlMessage();
            Messages = data.MysqlAllMessages();
            diagram = new Diagram();
            diagram.yachts = new List<List<int>>();
            diagram.devices = new List<List<int>>();
            Diagrams(0);
        }
        /// <summary>
        /// belső ablak választása
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (index != int.Parse(((Button)e.Source).Uid) || int.Parse(((Button)e.Source).Uid) == 2 || int.Parse(((Button)e.Source).Uid) == 3)
            {
                index = int.Parse(((Button)e.Source).Uid);
                GridCursor.Margin = new Thickness(10 + (135.5 * index), 45, 0, 329);
                Diagrams(index);
            }
        }
        /// <summary>
        /// Diagram paraméterek beállitásai és az adatok kikeresése
        /// </summary>
        /// <param name="index">ablak index</param>
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
                        if (Globals.User.login.admin)
                            diagram.oszlopY[i] = Messages.Where(x => string.Format("{0:yyyy,MMMM}", x.keletkezett).ToUpper() == (string.Format("{0:yyyy}", DateTime.Now)) + "," + 
                            month && x.elfogadvabl).Select(x => x.price).Sum();
                        else
                            diagram.oszlopY[i] = Messages.Where(x => string.Format("{0:yyyy,MMMM}", x.keletkezett).ToUpper() == (string.Format("{0:yyyy}", DateTime.Now)) + "," +
                            month && x.elfogadvabl && x.cimzett_id == Globals.User.member_id).Select(x => x.price).Sum();
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
                        if (Globals.User.login.admin)
                        {
                            diagram.oszlopY[i] = Messages.Count(x => string.Format("{0:yyyy,MMMM}", x.keletkezett).ToUpper() == (string.Format("{0:yyyy}", DateTime.Now)) + "," +
                            month && x.elfogadvabl);
                            diagram.yachts.Add(Messages.Where(x => string.Format("{0:yyyy,MMMM}", x.keletkezett).ToUpper() == (string.Format("{0:yyyy}", DateTime.Now)) + "," +
                            month && x.yacht_id != 0 && x.elfogadvabl).Select(x => x.yacht_id).ToList());
                            diagram.devices.Add(Messages.Where(x => string.Format("{0:yyyy,MMMM}", x.keletkezett).ToUpper() == (string.Format("{0:yyyy}", DateTime.Now)) + "," +
                            month && x.device_id != 0 && x.elfogadvabl).Select(x => x.device_id).ToList());
                        }
                        else
                        {
                            diagram.oszlopY[i] = Messages.Count(x => string.Format("{0:yyyy,MMMM}", x.keletkezett).ToUpper() == (string.Format("{0:yyyy}", DateTime.Now)) + "," +
                            month && x.elfogadvabl && x.cimzett_id == Globals.User.member_id);
                            diagram.yachts.Add(Messages.Where(x => string.Format("{0:yyyy,MMMM}", x.keletkezett).ToUpper() == (string.Format("{0:yyyy}", DateTime.Now)) + "," +
                            month && x.yacht_id != 0 && x.elfogadvabl && x.cimzett_id == Globals.User.member_id).Select(x => x.yacht_id).ToList());
                            diagram.devices.Add(Messages.Where(x => string.Format("{0:yyyy,MMMM}", x.keletkezett).ToUpper() == (string.Format("{0:yyyy}", DateTime.Now)) + "," +
                            month && x.device_id != 0 && x.elfogadvabl && x.cimzett_id == Globals.User.member_id).Select(x => x.device_id).ToList());
                        }
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
                        if (Globals.User.login.admin)
                            diagram.oszlopY[i] = Messages.Where(x => string.Format("{0:yyyy,MMMM,dddd}", x.keletkezett).ToUpper() == (string.Format("{0:yyyy,MMMM}", DateTime.Now).ToUpper()) + "," + 
                            day && x.elfogadvabl).Select(x => x.price).Sum();
                        else
                            diagram.oszlopY[i] = Messages.Where(x => string.Format("{0:yyyy,MMMM,dddd}", x.keletkezett).ToUpper() == (string.Format("{0:yyyy,MMMM}", DateTime.Now).ToUpper()) + "," +
                            day && x.elfogadvabl && x.cimzett_id == Globals.User.member_id).Select(x => x.price).Sum();
                    }
                    bevetel(false);
                    break;
                case 5://Heti bérlések
                    honap = 0;
                    diagram.oszlopC = 7;
                    diagram.oszlopX = (813 / 7) - 10;
                    diagram.oszlopY = new double[7];
                    diagram.sorN = new string[7];
                    diagram.yachts.Clear();
                    diagram.devices.Clear();
                    for (int i = 0; i < 7; i++)
                    {
                        days days = (days)i;
                        diagram.sorN[i] = days.ToString();
                        string day = diagram.sorN[i].ToUpper();
                        if (Globals.User.login.admin)
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
                    berles(false);
                    break;
            }
        }
        /// <summary>
        /// Diagram ablak generálás
        /// Bevételek ablak
        /// </summary>
        /// <param name="tabla">Kell e hónap tábla(havi egységek)</param>
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
                    StackPanel oszlop = new StackPanel();
                    oszlop.Width = diagram.oszlopX;
                    if (oszlop.Width < 25) oszlop.Width = 25;
                    oszlop.Height = diagram.oszlopY[i] * (270 / diagram.oszlopY.Max());
                    if (oszlop.Height != 0 && !(oszlop.Height > 0) || oszlop.Height == 0)
                        oszlop.Height = 10;
                    oszlop.Background = Globals.MainTheme.hatter;
                    oszlop.Margin = new Thickness(10, 0, 0, 0);
                    if (honap == 28) oszlop.Margin = new Thickness(4, 0, 0, 0);
                    if (honap == 30) oszlop.Margin = new Thickness(2, 0, 0, 0);
                    if (honap == 31) oszlop.Margin = new Thickness(1, 0, 0, 0);
                    oszlop.VerticalAlignment = VerticalAlignment.Bottom;
                    Label price = new Label();
                    if (diagram.oszlopY[i] > 0)
                        if (diagram.oszlopY[i].ToString().Length >= 3)
                            price.Content = splitCut(int.Parse(diagram.oszlopY[i].ToString()));
                        else
                            price.Content = diagram.oszlopY[i].ToString();
                    if (diagram.oszlopC == 12)
                    {
                        price.RenderTransform = new RotateTransform(-90, 0, 0);
                        price.Margin = new Thickness(15, 10 + (diagram.oszlopY[i].ToString().Length * 10), diagram.oszlopY[i].ToString().Length * (-10) + 25, 0);
                    }
                    if (diagram.oszlopC > 12 && diagram.oszlopY[i] > 0)
                    {
                        price.RenderTransform = new RotateTransform(-90, 0, 0);
                        price.Margin = new Thickness(0, 10 + (diagram.oszlopY[i].ToString().Length * 10), diagram.oszlopY[i].ToString().Length * (-10) + 25, 0);
                    }
                    price.Foreground = Globals.MainTheme.betu_szin;
                    price.HorizontalContentAlignment = HorizontalAlignment.Center;
                    oszlop.Children.Add(price);
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
        /// <summary>
        /// Diagram ablak generálás
        /// Bérlések ablak
        /// </summary>
        /// <param name="tabla">Kell e hónap tábla(havi egységek)</param>
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
                    oszlop.Height = diagram.oszlopY[i] * (270 / diagram.oszlopY.Max());
                    if (oszlop.Height != 0 && !(oszlop.Height > 0) || oszlop.Height == 0)
                        oszlop.Height = 10;
                    oszlop.Background = Globals.MainTheme.hatter;
                    oszlop.Margin = new Thickness(10, 0, 0, 0);
                    if (honap == 28) oszlop.Margin = new Thickness(4, 0, 0, 0);
                    if (honap == 30) oszlop.Margin = new Thickness(2, 0, 0, 0);
                    if (honap == 31) oszlop.Margin = new Thickness(1, 0, 0, 0);
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
                                if (devyac.Height < 20)
                                    devyac.Height = 23;
                                devyac.Background = Globals.MainTheme.uc_hatter;
                                if (diagram.oszlopC > 12)
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
                                if (devyac.Height < 20)
                                    devyac.Height = 23;
                                devyac.Background = Globals.MainTheme.uc_hatter;
                                if (diagram.oszlopC > 12)
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
        /// <summary>
        /// Táblázat generálás
        /// </summary>
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
        /// <summary>
        /// A kiválasztot táblázat id alapján elkésziteni a diagram paramétereit
        /// és az adatok kigyüjtése
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            diagram.yachts.Clear();
            diagram.devices.Clear();
            for (int i = 0; i < honap; i++)
            {
                diagram.sorN[i] = (i + 1).ToString();
                months months = (months)int.Parse(((Button)sender).Uid);
                string month = months.ToString().ToUpper();
                if (!bevet)
                {
                    if (Globals.User.login.admin)
                    {
                        diagram.oszlopY[i] = Messages.Count(x => string.Format("{0:yyyy,MMMM,d}", x.keletkezett).ToUpper() == (string.Format("{0:yyyy}", DateTime.Now).ToUpper()) + "," +
                        month + "," + diagram.sorN[i] && x.elfogadvabl);
                        diagram.yachts.Add(Messages.Where(x => string.Format("{0:yyyy,MMMM,d}", x.keletkezett).ToUpper() == (string.Format("{0:yyyy}", DateTime.Now).ToUpper()) + "," +
                        month + "," + diagram.sorN[i] && x.yacht_id != 0 && x.elfogadvabl).Select(x => x.yacht_id).ToList());
                        diagram.devices.Add(Messages.Where(x => string.Format("{0:yyyy,MMMM,d}", x.keletkezett).ToUpper() == (string.Format("{0:yyyy}", DateTime.Now).ToUpper()) + "," +
                        month + "," + diagram.sorN[i] && x.device_id != 0 && x.elfogadvabl).Select(x => x.device_id).ToList());
                    }
                    else
                    {
                        diagram.oszlopY[i] = Messages.Count(x => string.Format("{0:yyyy,MMMM,d}", x.keletkezett).ToUpper() == (string.Format("{0:yyyy}", DateTime.Now).ToUpper()) + "," +
                        month + "," + diagram.sorN[i] && x.elfogadvabl && x.cimzett_id == Globals.User.member_id);
                        diagram.yachts.Add(Messages.Where(x => string.Format("{0:yyyy,MMMM,d}", x.keletkezett).ToUpper() == (string.Format("{0:yyyy}", DateTime.Now).ToUpper()) + "," +
                        month + "," + diagram.sorN[i] && x.yacht_id != 0 && x.elfogadvabl && x.cimzett_id == Globals.User.member_id).Select(x => x.yacht_id).ToList());
                        diagram.devices.Add(Messages.Where(x => string.Format("{0:yyyy,MMMM,d}", x.keletkezett).ToUpper() == (string.Format("{0:yyyy}", DateTime.Now).ToUpper()) + "," +
                        month + "," + diagram.sorN[i] && x.device_id != 0 && x.elfogadvabl && x.cimzett_id == Globals.User.member_id).Select(x => x.device_id).ToList());
                    }
                } else
                    if (Globals.User.login.admin)
                        diagram.oszlopY[i] = Messages.Where(x => string.Format("{0:yyyy,MMMM,d}", x.keletkezett).ToUpper() == (string.Format("{0:yyyy}", DateTime.Now).ToUpper()) + "," + 
                        month + "," + diagram.sorN[i] && x.elfogadvabl).Select(x => x.price).Sum();
                    else
                        diagram.oszlopY[i] = Messages.Where(x => string.Format("{0:yyyy,MMMM,d}", x.keletkezett).ToUpper() == (string.Format("{0:yyyy}", DateTime.Now).ToUpper()) + "," +
                        month + "," + diagram.sorN[i] && x.elfogadvabl && x.cimzett_id == Globals.User.member_id).Select(x => x.price).Sum();
            }
            if (bevet)
                bevetel(false);
            else
                berles(false);
        }
        /// <summary>
        /// A kijelölt bérelt eszköz megjelenitése
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Pénzöszegek darbolása
        /// 1000000 => 1.000.000
        /// </summary>
        /// <param name="number">Darabolandó szám</param>
        /// <returns>Darabolt szám</returns>
        private string splitCut(int number)
        {
            string returnString = "";
            for (int j = 0; j <= number.ToString().Length / 3; j++)
            {
                if (j == 0)
                    returnString = number.ToString().Substring(number.ToString().Length - 3, 3) + returnString;
                else if (j == number.ToString().Length / 3)
                    returnString = number.ToString().Substring(0, number.ToString().Length % 3) + "." + returnString;
                else
                    returnString = number.ToString().Substring(number.ToString().Length - (3 * (j + 1)), 3) + "." + returnString;
            }
            return returnString;
        }
        /// <summary>
        /// Diagram paraméter structura
        /// </summary>
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
