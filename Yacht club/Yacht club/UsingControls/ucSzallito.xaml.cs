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
        private Device Device;

        public ucSzallito(int id)
        {
            InitializeComponent();
            data = new Database.MysqlDevice();
            if (Globals.selectedDevice != null && id == Globals.selectedDevice.id)
            {
                Device = Globals.selectedDevice;
            }
            else
            {
                Device = data.MysqlDeviceSelect(id);
                data = null;
            }
            Loading();
            Globals.UpdateHistory();
        }

        public void Loading()
        {
            if (Device.tipus != null)
                lbTipus.Content = Device.tipus;
            else lbTipus.Content = "Nincs kitöltve";

            if (Device.napi_ar != 0)
                lbNapiAr.Content = Device.napi_ar;
            else lbNapiAr.Content = "Nincs kitöltve";

            if (Device.full_name != null)
                lbTulajNev.Content = Device.full_name;
            else lbTulajNev.Content = "Nincs kitöltve";

            if (Device.berlo_full_name != null)
                lbBerloNev.Content = Device.berlo_full_name;
            else lbBerloNev.Content = "Nincs kitöltve";

            if (Device.max_hossz != "")
                lbMaxHossz.Content = Device.max_hossz;
            else lbMaxHossz.Content = "Nincs kitöltve";

            if (Device.max_suly != 0)
                lbMaxSuly.Content = Device.max_suly;
            else lbMaxSuly.Content = "Nincs kitöltve";

            if (Device.strfoglalt != null)
                lbFoglalt.Content = Device.strfoglalt;
            else lbFoglalt.Content = "Nincs kitöltve";


            if (Device.member_id == Globals.User.member_id || Globals.User.login.admin)
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
            lbMaxSuly.Visibility = System.Windows.Visibility.Hidden;
            lbFoglalt.Visibility = System.Windows.Visibility.Hidden;


            tbNapiAr.Visibility = System.Windows.Visibility.Visible;
            tbMaxHossz.Visibility = System.Windows.Visibility.Visible;
            tbMaxSuly.Visibility = System.Windows.Visibility.Visible;
            cbBerelheto.Visibility = System.Windows.Visibility.Visible;

            tbNapiAr.Text = Convert.ToString(Device.napi_ar);
            tbMaxHossz.Text = Convert.ToString(Device.max_hossz);
            tbMaxSuly.Text = Convert.ToString(Device.max_suly);

            if (Device.blfoglalt == false)
            {
                cbBerelheto.IsChecked = false;
            }
            else
            {
                cbBerelheto.IsChecked = true;
            }

        }



        private void btRendel_Click(object sender, RoutedEventArgs e)
        {
            Globals.Main.ccWindow_Main.Content = new ucBerles(2);
        }

        private void btMegse_Click(object sender, RoutedEventArgs e)
        {
            btRendel.Visibility = System.Windows.Visibility.Hidden;
            btModosit.Visibility = System.Windows.Visibility.Visible;
            btAlkalmaz.Visibility = System.Windows.Visibility.Hidden;
            btMegse.Visibility = System.Windows.Visibility.Hidden;


            lbNapiAr.Visibility = System.Windows.Visibility.Visible;
            lbMaxHossz.Visibility = System.Windows.Visibility.Visible;
            lbMaxSuly.Visibility = System.Windows.Visibility.Visible;
            lbFoglalt.Visibility = System.Windows.Visibility.Visible;


            tbNapiAr.Visibility = System.Windows.Visibility.Hidden;
            tbMaxHossz.Visibility = System.Windows.Visibility.Hidden;
            tbMaxSuly.Visibility = System.Windows.Visibility.Hidden;
            cbBerelheto.Visibility = System.Windows.Visibility.Hidden;

        }

        private void btAlkalmaz_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbBerelheto.IsChecked == true) Device.berelheto = true;
                else Device.berelheto = false;
                if (tbMaxHossz.Text != "") Device.max_hossz = tbMaxHossz.Text;
                if (tbMaxSuly.Text != "") Device.max_suly = int.Parse(tbMaxSuly.Text);
                if (tbNapiAr.Text != "") Device.napi_ar = int.Parse(tbNapiAr.Text);
                data.MysqlUpdateDevicet(Device);

            }
            catch (Exception)
            {
                Globals.log = "Módosítás Sikertelen! <Szállitóeszköz>";
            }

            btRendel.Visibility = System.Windows.Visibility.Hidden;
            btModosit.Visibility = System.Windows.Visibility.Visible;
            btAlkalmaz.Visibility = System.Windows.Visibility.Hidden;
            btMegse.Visibility = System.Windows.Visibility.Hidden;

            lbNapiAr.Content = tbNapiAr.Text;
            lbMaxHossz.Content = tbMaxHossz.Text;
            lbMaxSuly.Content = tbMaxSuly.Text;
            lbFoglalt.Content = strCheckboxErtek();

            lbNapiAr.Visibility = System.Windows.Visibility.Visible;
            lbMaxHossz.Visibility = System.Windows.Visibility.Visible;
            lbMaxSuly.Visibility = System.Windows.Visibility.Visible;
            lbFoglalt.Visibility = System.Windows.Visibility.Visible;


            tbNapiAr.Visibility = System.Windows.Visibility.Hidden;
            tbMaxHossz.Visibility = System.Windows.Visibility.Hidden;
            tbMaxSuly.Visibility = System.Windows.Visibility.Hidden;
            cbBerelheto.Visibility = System.Windows.Visibility.Hidden;

            Globals.log = "Módosítás Sikeres! <Szállitóeszköz>";
            Globals.Main.logAdd(true);
        }

        private string strCheckboxErtek()
        {
            if (cbBerelheto.IsChecked == true)
                return "Foglalt";
            else return "Szabad";
        }
        private int checkboxErtek()
        {
            if (cbBerelheto.IsChecked == true)
                return 1;
            else return 0;
        }
    }
}
