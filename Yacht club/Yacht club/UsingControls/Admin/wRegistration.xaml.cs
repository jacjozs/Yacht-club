using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Yacht_club
{
    /// <summary>
    /// Interaction logic for wRegistration.xaml
    /// </summary>
    public partial class wRegistration : Window
    {
        private Felhasznalo user;
        private Database.MysqlRegistration data;

        private void Themes_Loading(object sender, RoutedEventArgs e)
        {
            DataContext = Globals.MainTheme;
        }
        public wRegistration()
        {
            InitializeComponent();
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Globals.Main.MainWindow.Opacity = 1;
            Globals.Main.logAdd(false);
            this.Hide();
        }

        private void btRegiszt_Click(object sender, RoutedEventArgs e)
        {
            data = new Database.MysqlRegistration();
            Login login = new Login();
            user = new Felhasznalo();
            try
            {
                user.veztek_nev = tbFirstName.Text;
                user.kereszt_nev = tbLastName.Text;
                if (dpSzuletes.SelectedDate.HasValue)
                user.szuletesdt = dpSzuletes.SelectedDate.Value;
                user.nickname = tbNickName.Text;
                login.felhasznalonev = tbLoginName.Text;
                login.jelszo = pbPasswd.Password;
                login.email = tbEmail.Text;
                user.login = login;
                user.varos = tbVaros.Text;
                user.iranyitoszm = int.Parse(tbIranyitoszm.Text);
                user.lakcim = tbLakcim.Text;
                user.orszag = tbOrszg.Text;
                if (cbAdmin.IsChecked == true)
                user.login.admin = true;
                else user.login.admin = false;
                data.MysqlRegisztracioLogin(user);
            }
            catch (MySqlException)
            {
                Globals.log = "Sikertelen Regisztráció!";
            }
            finally
            {
                Globals.log = "Sikeres Regisztráció!";
            }
            Globals.Main.MainWindow.Opacity = 1;
            Globals.Main.logAdd(true);
            this.Hide();
        }

        private bool ellenorzes()
        {
            if (pbPasswd.Password != pbRePasswd.Password)
                return false;
            return true;
        }
    }
}
