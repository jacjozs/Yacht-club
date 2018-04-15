using System;
using System.Windows;
using System.Windows.Input;

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
            tbLoginName.Focus();
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
            data = null;
            if (user_login != null)
            {
                Globals.User = user_login;
                Globals.MainTheme = new Moduls.Themes(user_login.login.theme);
                Globals.Main = new Main_Yacht_Window();
                Globals.Main.lbNickname.Content = user_login.nickname + "!";
                Globals.User.login.utolsoLogin = DateTime.Now.Date;
                if (Globals.User.login.admin)
                    Globals.Main.dpRegist.Visibility = Visibility.Visible;
                Globals.Main.logAdd(false);
                //A login ablak eltüntetése
                this.Hide();
                Globals.Main.Show();
            }
            switch (Globals.Login_Hiba_Code)
            {
                case 1:
                        MessageBox.Show("Hibás felhasználónév", "Hiba!", MessageBoxButton.OK);
                        break;
                case 2:
                        MessageBox.Show("Hibás jelszó", "Hiba!", MessageBoxButton.OK);
                        break;
            }
        }
        /// <summary>
        /// Kilépés a programból
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        /// <summary>
        /// Enter lenyomása akció
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Login_Window_Enter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (tbLoginName.IsFocused)
                    pbPasswd.Focus();
                else
                    Login();
            }
        }
        /// <summary>
        /// ablak mozgatása
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Moveing_Click(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
