using MySql.Data.MySqlClient;
using System;
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
using System.Windows.Shapes;

namespace Yacht_club
{
    /// <summary>
    /// Interaction logic for wRegistration.xaml
    /// </summary>
    public partial class wRegistration : Window
    {
        public Main_Yacht_Window register;
        internal Felhasznalo user { get; set; }
        private Database data;

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
            register = new Main_Yacht_Window();
            register.user = user;
            register.tbNickname.Text = user.nickname + "!";
            if (user.login.admin) register.dpRegist.Visibility = Visibility.Visible;
            register.Owner = this;
            this.Hide();
            register.ShowDialog();
        }

        private void btRegiszt_Click(object sender, RoutedEventArgs e)
        {
            if (ellenorzes())
            {
                data = new Database();
                data.MysqlConnect();
                Login login = new Login();
                register = new Main_Yacht_Window();
                try
                {
                    register.user_Register.veztek_nev = tbFirstName.Text;
                    register.user_Register.kereszt_nev = tbLastName.Text;
                    if (dpSzuletes.SelectedDate.HasValue)
                    {
                        register.user_Register.szuletesdt = dpSzuletes.SelectedDate.Value;
                    }
                    register.user_Register.nickname = tbNickName.Text;
                    login.felhasznalonev = tbLoginName.Text;
                    login.jelszo = pbPasswd.Password;
                    login.email = tbEmail.Text;
                    register.user_Register.login = login;
                    register.user_Register.varos = tbVaros.Text;
                    register.user_Register.iranyitoszm = int.Parse(tbIranyitoszm.Text);
                    register.user_Register.lakcim = tbLakcim.Text;
                    register.user_Register.orszag = tbOrszg.Text;
                    if (cbAdmin.IsChecked == true)
                    {
                        register.user_Register.login.admin = true;
                    } else register.user_Register.login.admin = false;
                    data.MysqlRegisztracioLogin(register.user_Register);
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error in adding mysql row. Error: " + ex.Message);
                }
            } else { MessageBox.Show("Hibás jelszó"); return; }
            if (user.login.admin) { register.dpRegist.Visibility = Visibility.Visible; }
            register.tbNickname.Text = user.nickname + "!";
            register.user = user;
            register.Owner = this;
            this.Hide();
            register.ShowDialog();
        }

        private void dpSzuletes_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var picker = sender as DatePicker;
            DateTime? date = picker.SelectedDate;
            if (date == null)
                this.Title = "Nincs Dátum";
            else
                this.Title = date.Value.ToShortDateString();
        }

        private bool ellenorzes()
        {
            if (pbPasswd.Password != pbRePasswd.Password)
                return false;
            return true;
        }
    }
}
