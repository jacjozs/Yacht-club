using MySql.Data.MySqlClient;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Yacht_club.UsingControls
{
    /// <summary>
    /// Interaction logic for ucSzallito.xaml
    /// </summary>
    public partial class ucSzallito : UserControl
    {
        private Database.MysqlDevice data;

        public ucSzallito()
        {
            InitializeComponent();
            Loading();
        }

        public void Loading()
        {
            if (Globals.selectedDevice.tipus != null)
                lbTipus.Content = Globals.selectedDevice.tipus;
            else lbTipus.Content = "Nincs kitöltve";

            if (Globals.selectedDevice.napi_ar != 0)
                lbNapiAr.Content = Globals.selectedDevice.napi_ar;
            else lbNapiAr.Content = "Nincs kitöltve";

            if (Globals.selectedDevice.full_name != null)
                lbTulajNev.Content = Globals.selectedDevice.full_name;
            else lbTulajNev.Content = "Nincs kitöltve";

            if (Globals.selectedDevice.berlo_full_name != null)
                lbBerloNev.Content = Globals.selectedDevice.berlo_full_name;
            else lbBerloNev.Content = "Nincs kitöltve";

            if (Globals.selectedDevice.max_hossz != 0)
                lbMaxHossz.Content = Globals.selectedDevice.max_hossz;
            else lbMaxHossz.Content = "Nincs kitöltve";

            if (Globals.selectedDevice.max_magas != 0)
                lbMaxMagas.Content = Globals.selectedDevice.max_magas;
            else lbMaxMagas.Content = "Nincs kitöltve";

            if (Globals.selectedDevice.max_suly != 0)
                lbMaxSuly.Content = Globals.selectedDevice.max_suly;
            else lbMaxSuly.Content = "Nincs kitöltve";

            if (Globals.selectedDevice.max_szeles != 0)
                lbMaxSzeles.Content = Globals.selectedDevice.max_szeles;
            else lbMaxSzeles.Content = "Nincs kitöltve";

            if (Globals.selectedDevice.strfoglalt != null)
                lbFoglalt.Content = Globals.selectedDevice.strfoglalt;
            else lbFoglalt.Content = "Nincs kitöltve";


            if (Globals.selectedDevice.member_id == Globals.User.member_id || Globals.User.login.admin)
            {
                btModosit.Visibility = Visibility.Visible;
            }
            else
            {
                btModosit.Visibility = Visibility.Hidden;
                btRendel.Visibility = Visibility.Visible;
            }

            data = new Database.MysqlDevice();

        }

        private void btModosit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            btRendel.Visibility = System.Windows.Visibility.Hidden;
            btModosit.Visibility = System.Windows.Visibility.Hidden;
            btAlkalmaz.Visibility = System.Windows.Visibility.Visible;
            btMegse.Visibility = System.Windows.Visibility.Visible;

            lbNapiAr.Visibility = System.Windows.Visibility.Hidden;
            lbMaxHossz.Visibility = System.Windows.Visibility.Hidden;
            lbMaxMagas.Visibility = System.Windows.Visibility.Hidden;
            lbMaxSuly.Visibility = System.Windows.Visibility.Hidden;
            lbMaxSzeles.Visibility = System.Windows.Visibility.Hidden;
            lbFoglalt.Visibility = System.Windows.Visibility.Hidden;


            tbNapiAr.Visibility = System.Windows.Visibility.Visible;
            tbMaxHossz.Visibility = System.Windows.Visibility.Visible;
            tbMaxMagas.Visibility = System.Windows.Visibility.Visible;
            tbMaxSuly.Visibility = System.Windows.Visibility.Visible;
            tbMaxSzeles.Visibility = System.Windows.Visibility.Visible;
            cbBerelheto.Visibility = System.Windows.Visibility.Visible;

            tbNapiAr.Text = Convert.ToString(Globals.selectedDevice.napi_ar);
            tbMaxHossz.Text = Convert.ToString(Globals.selectedDevice.max_hossz);
            tbMaxMagas.Text = Convert.ToString(Globals.selectedDevice.max_magas);
            tbMaxSuly.Text = Convert.ToString(Globals.selectedDevice.max_suly);
            tbMaxSzeles.Text = Convert.ToString(Globals.selectedDevice.max_szeles);

            if (Globals.selectedDevice.blfoglalt == false)
            {
                cbBerelheto.IsChecked = false;
            }
            else
            {
                cbBerelheto.IsChecked = true;
            }

        }



        private void btRendel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Globals.Main.ccWindow_2.Content = new ucBerles();
        }

        private void btMegse_Click(object sender, RoutedEventArgs e)
        {
            btRendel.Visibility = System.Windows.Visibility.Hidden;
            btModosit.Visibility = System.Windows.Visibility.Visible;
            btAlkalmaz.Visibility = System.Windows.Visibility.Hidden;
            btMegse.Visibility = System.Windows.Visibility.Hidden;


            lbNapiAr.Visibility = System.Windows.Visibility.Visible;
            lbMaxHossz.Visibility = System.Windows.Visibility.Visible;
            lbMaxMagas.Visibility = System.Windows.Visibility.Visible;
            lbMaxSuly.Visibility = System.Windows.Visibility.Visible;
            lbMaxSzeles.Visibility = System.Windows.Visibility.Visible;
            lbFoglalt.Visibility = System.Windows.Visibility.Visible;


            tbNapiAr.Visibility = System.Windows.Visibility.Hidden;
            tbMaxHossz.Visibility = System.Windows.Visibility.Hidden;
            tbMaxMagas.Visibility = System.Windows.Visibility.Hidden;
            tbMaxSuly.Visibility = System.Windows.Visibility.Hidden;
            tbMaxSzeles.Visibility = System.Windows.Visibility.Hidden;
            cbBerelheto.Visibility = System.Windows.Visibility.Hidden;

        }

        private void btAlkalmaz_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbBerelheto.IsChecked == true) Globals.selectedDevice.berelheto = true;
                else Globals.selectedDevice.berelheto = false;
                if (tbMaxSzeles.Text != "") Globals.selectedDevice.max_szeles = int.Parse(tbMaxSzeles.Text);
                if (tbMaxHossz.Text != "") Globals.selectedDevice.max_hossz = int.Parse(tbMaxHossz.Text);
                if (tbMaxMagas.Text != "") Globals.selectedDevice.max_magas = int.Parse(tbMaxMagas.Text);
                if (tbMaxSuly.Text != "") Globals.selectedDevice.max_suly = int.Parse(tbMaxSuly.Text);
                if (tbNapiAr.Text != "") Globals.selectedDevice.napi_ar = int.Parse(tbNapiAr.Text);
                data.MysqlUpdateDevicet(Globals.selectedDevice);

            }
            catch (Exception)
            {
                Globals.log = "Módosítás Sikertelen! <Szállitóeszköz>";
            }

            btRendel.Visibility = System.Windows.Visibility.Hidden;
            btModosit.Visibility = System.Windows.Visibility.Visible;
            btAlkalmaz.Visibility = System.Windows.Visibility.Hidden;
            btMegse.Visibility = System.Windows.Visibility.Hidden;

            //még1x
            lbNapiAr.Content = tbNapiAr.Text;
            lbMaxHossz.Content = tbMaxHossz.Text;
            lbMaxMagas.Content = tbMaxMagas.Text;
            lbMaxSuly.Content = tbMaxMagas.Text;
            lbMaxSzeles.Content = tbMaxSzeles.Text;
            lbFoglalt.Content = strCheckboxErtek();

            lbNapiAr.Visibility = System.Windows.Visibility.Visible;
            lbMaxHossz.Visibility = System.Windows.Visibility.Visible;
            lbMaxMagas.Visibility = System.Windows.Visibility.Visible;
            lbMaxSuly.Visibility = System.Windows.Visibility.Visible;
            lbMaxSzeles.Visibility = System.Windows.Visibility.Visible;
            lbFoglalt.Visibility = System.Windows.Visibility.Visible;


            tbNapiAr.Visibility = System.Windows.Visibility.Hidden;
            tbMaxHossz.Visibility = System.Windows.Visibility.Hidden;
            tbMaxMagas.Visibility = System.Windows.Visibility.Hidden;
            tbMaxSuly.Visibility = System.Windows.Visibility.Hidden;
            tbMaxSzeles.Visibility = System.Windows.Visibility.Hidden;
            cbBerelheto.Visibility = System.Windows.Visibility.Hidden;

            Globals.log = "Módosítás Sikeres! <Szállitóeszköz>";
            Globals.Main.logAdd(true);
        }

        private string strCheckboxErtek()
        {
            if (cbBerelheto.IsChecked == true)
            {
                return "Foglalt";
            }
            else return "Szabad";
        }
        private int checkboxErtek()
        {
            if (cbBerelheto.IsChecked == true)
            {
                return 1;
            }
            else return 0;
        }
    }
}
