using System.Drawing.Imaging;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows;
using MySql.Data.MySqlClient;
using System;
using Microsoft.Win32;
using System.Collections.Generic;
using Yacht_club.Moduls;
using Yacht_club.Database;

namespace Yacht_club.UsingControls
{
    /// <summary>
    /// Interaction logic for ucYacht.xaml
    /// </summary>
    public partial class ucYacht : UserControl
    {
        private Label[] lbAdatok;
        private TextBox[] tbAdatok;
        private Yacht Yacht;
        private MysqlYacht data;
        private Dictionary<int, string> ports;

        public ucYacht(int id)
        {
            // label tömb létrehozása, adatokkal való feltöltése

            InitializeComponent();
            data = new MysqlYacht();
            if (Globals.selectedYacht != null && id == Globals.selectedYacht.id)
            {
                Yacht = Globals.selectedYacht;
            } else
            {
                Yacht = data.MysqlYachtSelect(id);
            }
            lbAdatok = new Label[]
            {
                lbNev,lbMerul,lbHossz,lbFoglalt,lbAr,lbBerelheto,lbSeb,lbUlesek,lbHely,lbSzel,lbGyarto
            };
            Port_name();
            YachtImgLoad(false);
            LabelFeltolt();
            lbId.Content = Yacht.id.ToString();

            //Felhasználó id és yacht id összehasonlítása

            if (Globals.User.member_id == Yacht.member_id || Globals.User.login.admin)
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
            Globals.Main.ccWindow_Main.Content = new ucBerles(1);
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
            TbKikotok.Visibility = Visibility.Visible;
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

            tbNev.Text = Yacht.nev;
            tbGyarto.Text = Yacht.gyarto;
            tbÜlesek.Text = Yacht.ferohely.ToString();
            tbSeb.Text = Yacht.sebesseg.ToString();
            tbMerul.Text = Yacht.merules.ToString();
            tbHossz.Text = Yacht.hossz.ToString();
            tbSzel.Text = Yacht.szeles.ToString();
            tbAr.Text = Yacht.napi_ar.ToString();
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
                if (tbNev.Text != "") Yacht.nev = tbNev.Text;
                if (tbÜlesek.Text != "") Yacht.ferohely = int.Parse(tbÜlesek.Text);
                if (tbSzel.Text != "") Yacht.szeles = (float)Convert.ToDouble(tbSzel.Text);
                if (tbHossz.Text != "") Yacht.hossz = (float)Convert.ToDouble(tbHossz.Text);
                if (tbMerul.Text != "") Yacht.merules = (float)Convert.ToDouble(tbMerul.Text);
                if (tbSeb.Text != "") Yacht.sebesseg = int.Parse(tbSeb.Text);
                if (tbAr.Text != "") Yacht.napi_ar = int.Parse(tbAr.Text);
                if (TbKikotok.Text != "")
                {
                    Yacht.kikoto_id = TbKikotok.ID;
                    Yacht.kikoto = TbKikotok.Text;
                }
                if (tbjImage.Text != "") Yacht.kep = new BitmapImage(new Uri(tbjImage.Text));
                Yacht.berelheto = cbBerelheto.IsChecked.Value;
                data.MysqlUpdateYacht(Yacht);
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
                TbKikotok.Visibility = Visibility.Hidden;
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
            TbKikotok.Visibility = Visibility.Hidden;
            lbBerelheto.Visibility = Visibility.Visible;
            KikapcsolTB();
            LabelFeltolt();
        }
        /// <summary>
        /// Labelek feltöltése a yacht adatokal
        /// </summary>
        private void LabelFeltolt()
        { // Labelek feltöltése adatokkal
            lbNev.Content = Yacht.nev;
            lbGyarto.Content = Yacht.gyarto;
            lbUlesek.Content = Yacht.ferohely;
            lbSeb.Content = Yacht.sebesseg;
            lbMerul.Content = Yacht.merules;
            lbHossz.Content = Yacht.hossz;
            lbSzel.Content = Yacht.szeles;
            if (Yacht.blfoglalt) lbFoglalt.Content = "Foglalt"; else lbFoglalt.Content = "Szabad";
            if (!Yacht.berelheto) lbBerelheto.Content = "Nem bérelhető"; else lbBerelheto.Content = "Bérelhető";
            lbAr.Content = Yacht.napi_ar;
            lbHely.Content = Yacht.kikoto;
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
            ports = MysqlGeneral.MysqlPortName();
            foreach (var entry in ports)
            {
                TbKikotok.AddItem(new AutoCompleteEntry(entry.Key, entry.Value, Globals.cut(entry.Value)));
            }
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
                Yacht.kep = data.MysqlYachtImage(Yacht.id);
            if (Yacht.kep != null)
            {
                imgYacht.Source = Yacht.kep;
            }
        }
    }
}