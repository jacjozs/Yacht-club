using System.Drawing.Imaging;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows;
using MySql.Data.MySqlClient;
using System;
using Microsoft.Win32;
using System.Collections.Generic;

namespace Yacht_club.UsingControls
{
    /// <summary>
    /// Interaction logic for ucYacht.xaml
    /// </summary>
    public partial class ucYacht : UserControl
    {
        private Label[] lbAdatok;
        private TextBox[] tbAdatok;
        private Database.MysqlYacht data;
        private Dictionary<string, int> ports;

        public ucYacht()
        {
            // label tömb létrehozása, adatokkal való feltöltése

            InitializeComponent();
            data = new Database.MysqlYacht();
            lbAdatok = new Label[]
            {
                lbNev,lbMerul,lbHossz,lbFoglalt,lbAr,lbBerelheto,lbSeb,lbUlesek,lbHely,lbSzel,lbGyarto
            };
            Port_name();
            YachtImgLoad(false);
            LabelFeltolt();

            //Felhasználó id és yacht id összehasonlítása

            if (Globals.User.member_id == Globals.selectedYacht.member_id || Globals.User.login.admin)
            {
                btModosit.Visibility = Visibility.Visible;
                btBerles.Visibility = Visibility.Hidden;
            }
            Globals.UpdateHistory();
        }

        /// <summary>
        /// Yacht foglalási ablak betöltése
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clkFoglal(object sender, RoutedEventArgs e)
        {
            Globals.Main.ccWindow_Main.Content = new ucBerles();
        }

        /// <summary>
        /// textboxok,checkbox létrehozása, aktiválása és megjelenítése
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clkModosit(object sender, RoutedEventArgs e)
        {
            btBerles.Visibility = Visibility.Hidden;
            btAlkalmaz.Visibility = Visibility.Visible;
            btModosit.Visibility = Visibility.Hidden;
            btMegse.Visibility = Visibility.Visible;
            cbJHely.Visibility = Visibility.Visible;
            btImage.Visibility = Visibility.Visible;

            tbAdatok = new TextBox[]
            {
                 tbAr,tbHossz,tbMerul,tbNev,tbSeb,tbSzel,tbGyarto,tbÜlesek
            };

            lbBerelheto.Visibility = Visibility.Hidden;

            tbAr.IsEnabled = true;
            tbNev.IsEnabled = true;
            cbBerelheto.IsEnabled = true;
            tbSeb.IsEnabled = true;
            tbMerul.IsEnabled = true;
            tbSzel.IsEnabled = true;
            tbHossz.IsEnabled = true;
            tbÜlesek.IsEnabled = true;

            tbAr.Visibility = Visibility.Visible;
            tbNev.Visibility = Visibility.Visible;
            cbBerelheto.Visibility = Visibility.Visible;
            tbSeb.Visibility = Visibility.Visible;
            tbMerul.Visibility = Visibility.Visible;
            tbSzel.Visibility = Visibility.Visible;
            tbHossz.Visibility = Visibility.Visible;
            tbÜlesek.Visibility = Visibility.Visible;
            tbjImage.Visibility = Visibility.Visible;

            tbNev.Text = Globals.selectedYacht.nev;
            tbGyarto.Text = Globals.selectedYacht.gyarto;
            tbÜlesek.Text = Globals.selectedYacht.ferohely.ToString();
            tbSeb.Text = Globals.selectedYacht.sebesseg.ToString();
            tbMerul.Text = Globals.selectedYacht.merules.ToString();
            tbHossz.Text = Globals.selectedYacht.hossz.ToString();
            tbSzel.Text = Globals.selectedYacht.szeles.ToString();
            tbAr.Text = Globals.selectedYacht.napi_ar.ToString();
        }
        /// <summary>
        /// A módosítások alkalmázása és az adatbázisbavaló betöltés
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clkAlkalmaz(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbNev.Text != "") Globals.selectedYacht.nev = tbNev.Text;
                if (tbÜlesek.Text != "") Globals.selectedYacht.ferohely = int.Parse(tbÜlesek.Text);
                if (tbSzel.Text != "") Globals.selectedYacht.szeles = (float)Convert.ToDouble(tbSzel.Text);
                if (tbHossz.Text != "") Globals.selectedYacht.hossz = (float)Convert.ToDouble(tbHossz.Text);
                if (tbMerul.Text != "") Globals.selectedYacht.merules = (float)Convert.ToDouble(tbMerul.Text);
                if (tbSeb.Text != "") Globals.selectedYacht.sebesseg = int.Parse(tbSeb.Text);
                if (tbAr.Text != "") Globals.selectedYacht.napi_ar = int.Parse(tbAr.Text);
                if (cbJHely.Text != "")
                {
                    Globals.selectedYacht.kikoto_id = ports[cbJHely.Text];
                    Globals.selectedYacht.kikoto = cbJHely.Text;
                }
                if (tbjImage.Text != "") Globals.selectedYacht.kep = new BitmapImage(new Uri(tbjImage.Text));
                Globals.selectedYacht.berelheto = cbBerelheto.IsChecked.Value;
                data.MysqlUpdateYacht(Globals.selectedYacht);
                Globals.log = "Módosítás Sikeres! <Yacht>";
            }
            catch (Exception)
            {
                Globals.log = "Módosítás Sikertelen! <Yacht>";
            }
            finally
            {
                KikapcsolTB();
                LabelFeltolt();
                YachtImgLoad(true);

                btModosit.Visibility = Visibility.Visible;
                btAlkalmaz.Visibility = Visibility.Hidden;
                btImage.Visibility = Visibility.Hidden;
                tbjImage.Visibility = Visibility.Hidden;
                btMegse.Visibility = Visibility.Hidden;
                cbJHely.Visibility = Visibility.Hidden;
                lbBerelheto.Visibility = Visibility.Visible;
            }
            Globals.Main.logAdd(true);
        }
        /// <summary>
        /// textbox-ok kikapcsolása, elrejtése, labelek megjelenítése
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clkMegse(object sender, RoutedEventArgs e)
        {
            btMegse.Visibility = Visibility.Hidden;
            btModosit.Visibility = Visibility.Visible;
            btAlkalmaz.Visibility = Visibility.Hidden;
            btImage.Visibility = Visibility.Hidden;
            tbjImage.Visibility = Visibility.Hidden;
            cbJHely.Visibility = Visibility.Hidden;
            lbBerelheto.Visibility = Visibility.Visible;
            KikapcsolTB();
            LabelFeltolt();
        }
        /// <summary>
        /// Labelek feltöltése a yacht adatokal
        /// </summary>
        private void LabelFeltolt()
        { // Labelek feltöltése adatokkal
            lbNev.Content = Globals.selectedYacht.nev;
            lbGyarto.Content = Globals.selectedYacht.gyarto;
            lbUlesek.Content = Globals.selectedYacht.ferohely;
            lbSeb.Content = Globals.selectedYacht.sebesseg;
            lbMerul.Content = Globals.selectedYacht.merules;
            lbHossz.Content = Globals.selectedYacht.hossz;
            lbSzel.Content = Globals.selectedYacht.szeles;
            if (Globals.selectedYacht.blfoglalt) lbFoglalt.Content = "Foglalt"; else lbFoglalt.Content = "Szabad";
            if (!Globals.selectedYacht.berelheto) lbBerelheto.Content = "Nem bérelhető"; else lbBerelheto.Content = "Bérelhető";
            lbAr.Content = Globals.selectedYacht.napi_ar;
            lbHely.Content = Globals.selectedYacht.kikoto;
        }
        /// <summary>
        /// Textboxok elrejtése
        /// </summary>
        private void KikapcsolTB()
        { //Textboxok kikapcsolása és elrejtése
            tbAr.IsEnabled = false;
            tbNev.IsEnabled = false;
            cbBerelheto.IsEnabled = false;
            tbSeb.IsEnabled = false;
            tbMerul.IsEnabled = false;
            tbSzel.IsEnabled = false;
            tbHossz.IsEnabled = false;
            tbGyarto.IsEnabled = false;
            tbÜlesek.IsEnabled = false;
            tbJHely.IsEnabled = false;

            tbAr.Visibility = Visibility.Hidden;
            tbNev.Visibility = Visibility.Hidden;
            cbBerelheto.Visibility = Visibility.Hidden;
            tbSeb.Visibility = Visibility.Hidden;
            tbMerul.Visibility = Visibility.Hidden;
            tbSzel.Visibility = Visibility.Hidden;
            tbHossz.Visibility = Visibility.Hidden;
            tbGyarto.Visibility = Visibility.Hidden;
            tbÜlesek.Visibility = Visibility.Hidden;
            tbJHely.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// Kikötő nevek kigyüjtése és betöltése
        /// </summary>
        public void Port_name()
        {
            ports = data.MysqlYachtPortName();
            cbJHely.ItemsSource = ports.Keys;
        }
        /// <summary>
        /// Kép tallózási ablak megnyitása és kiírása az elérésiútnak
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clkImage(object sender, RoutedEventArgs e)
        {
            string filepath;
            OpenFileDialog open = new OpenFileDialog();
            open.InitialDirectory = "C:\\";
            open.Filter = "Image Files(*.PNG;*.JPG;*.JPEG;*.BMP;*.GIF)|*.PNG;*.JPG;*.JPEG;*.BMP;*.GIF";
            open.FilterIndex = 1;
            bool dialogok = open.ShowDialog().Value;

            if (dialogok)
            {
                filepath = open.FileName;
                tbjImage.Text = filepath;
            }
        }
        /// <summary>
        /// Yacht kép betöltése valamint frissitése
        /// </summary>
        /// <param name="reload">betölti a képet vagy frissiti</param>
        public void YachtImgLoad(bool reload)
        {
            if (!reload)
                Globals.selectedYacht.kep = data.MysqlYachtImage(Globals.selectedYacht.id);
            if (Globals.selectedYacht.kep != null)
            {
                imgYacht.Source = Globals.selectedYacht.kep;
            }
        }
    }
}