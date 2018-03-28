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
    /// Interaction logic for wLogin.xaml
    /// </summary>
    public partial class wLogin : Window
    {
        private Database.MysqlLogin data;
        public wLogin()
        {
            InitializeComponent();
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown();
        }
        /// <summary>
        /// Itt kellene majd a bejelentkezést ellenörizni valamint a hibákat kiíni
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btBelepes_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void Login()
        {
            data = new Database.MysqlLogin();
            //A kikereset és ellenörzött adatok bevitele a main user változoba
            //Egyfajta átadás
            Felhasznalo user_login = data.MysqlFelhasznalo(tbLoginName.Text, pbPasswd.Password);
            if (user_login != null)
            {
                Globals.OldThemeId = user_login.login.theme;
                Globals.MainTheme = new Moduls.Themes(Globals.OldThemeId);
                Globals.Main = new Main_Yacht_Window();
                Globals.User = user_login;
                Globals.Main.lbNickname.Content = user_login.nickname + "!";
                Globals.User.login.utolsoLogin = DateTime.Now.Date;
                if (Globals.User.login.admin)
                    Globals.Main.dpRegist.Visibility = Visibility.Visible;
                Globals.Main.logAdd(false);
                //A login ablak eltüntetése
                Globals.Main.Owner = this;
                this.Hide();
                Globals.Main.ShowDialog();
            }
            else
                MessageBox.Show("Hibás felhasználónév vagy jelszó", "Hiba!", MessageBoxButton.OK);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void tbLoginName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Login();
        }
    }
}
