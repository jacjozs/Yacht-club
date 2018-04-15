using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Yacht_club.Database;

namespace Yacht_club
{
    /// <summary>
    /// Interaction logic for wRegistration.xaml
    /// </summary>
    public partial class wRegistration : Window
    {
        private Felhasznalo user;
        private Dictionary<int, string> zipCodes;
        private List<string> loginNames;
        private List<string> emails;
        private MysqlRegistration data;

        private void Themes_Loading(object sender, RoutedEventArgs e)
        {
            DataContext = Globals.MainTheme;
        }
        public wRegistration()
        {
            InitializeComponent();
            zipCodes = MysqlGeneral.MysqlZipCodeName();
            loginNames = MysqlGeneral.MysqlLoginNames();
            emails = MysqlGeneral.MysqlEmails();
            btRegiszt.IsEnabled = false;
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
             Regisztral();
        }

        private void Regisztral()
        {
            data = new MysqlRegistration();
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
                Globals.log = "Sikeres Regisztráció!<Regisztráció>";
            }
            catch (MySqlException)
            {
                Globals.log = "Sikertelen Regisztráció!<Regisztráció>";
            }
            finally
            {
                data = null;
                Globals.Main.MainWindow.Opacity = 1;
                Globals.Main.logAdd(true);
                this.Hide();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                     Regisztral();
                    break;
                case Key.Escape:
                case Key.Back:
                    Globals.Main.MainWindow.Opacity = 1;
                    Globals.Main.logAdd(false);
                    this.Hide();
                    break;
            }
        }

        private void Moveing_Click(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void tbIranyitoszm_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (zipCodes.ContainsKey(int.Parse(tbIranyitoszm.Text)))
                {
                    imgZipError.Visibility = Visibility.Hidden;
                    tbVaros.Text = zipCodes[int.Parse(tbIranyitoszm.Text)];
                    btRegiszt.IsEnabled = true;
                    btRegiszt.Opacity = 1;
                }
                else
                {
                    tbVaros.Text = "";
                    imgZipError.Visibility = Visibility.Visible;
                    btRegiszt.IsEnabled = false;
                    btRegiszt.Opacity = 0.6;
                }
            }
            catch (Exception)
            {
                tbVaros.Text = "";
                imgZipError.Visibility = Visibility.Visible;
                btRegiszt.IsEnabled = false;
                btRegiszt.Opacity = 0.6;
            }
        }

        private void pbRePasswd_LostFocus(object sender, RoutedEventArgs e)
        {
            if (pbPasswd.Password != pbRePasswd.Password)
            {
                imgPassError.Visibility = Visibility.Visible;
                btRegiszt.IsEnabled = false;
                btRegiszt.Opacity = 0.6;
            }
            else
            {
                imgPassError.Visibility = Visibility.Hidden;
                btRegiszt.IsEnabled = true;
                btRegiszt.Opacity = 1;
            }
        }

        private void tbLoginName_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (loginNames.Contains(tbLoginName.Text))
                {
                    imgLoginError.Visibility = Visibility.Visible;
                    btRegiszt.IsEnabled = false;
                    btRegiszt.Opacity = 0.6;
                }
                else
                {
                    imgLoginError.Visibility = Visibility.Hidden;
                    btRegiszt.IsEnabled = true;
                    btRegiszt.Opacity = 1;
                }
            }
            catch (Exception)
            {
                imgLoginError.Visibility = Visibility.Visible;
                btRegiszt.IsEnabled = false;
                btRegiszt.Opacity = 0.6;
            }
        }

        private void tbEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (emails.Contains(tbEmail.Text))
                {
                    imgEmailError.Visibility = Visibility.Visible;
                    btRegiszt.IsEnabled = false;
                    btRegiszt.Opacity = 0.6;
                }
                else
                {
                    imgEmailError.Visibility = Visibility.Hidden;
                    btRegiszt.IsEnabled = true;
                    btRegiszt.Opacity = 1;
                }
            }
            catch (Exception)
            {
                imgEmailError.Visibility = Visibility.Visible;
                btRegiszt.IsEnabled = false;
                btRegiszt.Opacity = 0.6;
            }
        }
    }
}
