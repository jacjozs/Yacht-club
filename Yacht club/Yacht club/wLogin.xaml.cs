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
        private Main_Yacht_Window Main;
        private Database data;
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
            data = new Database();
            Main = new Main_Yacht_Window();
            //A kikereset és ellenörzött adatok bevitele a main user változoba
            //Egyfajta átadás
            Felhasznalo user_login = data.MysqlFelhasznalo(tbLoginName.Text, pbPasswd.Password);
            if (user_login != null)
            {
                Globals.User = user_login;
                Main.lbNickname.Content = user_login.nickname + "!";
                Globals.User.login.utolsoLogin = DateTime.Now.Date;
                if (Globals.User.login.admin) { Main.dpRegist.Visibility = Visibility.Visible; }
                Main.logAdd(false);
                //A login ablak eltünéséhez szükséges
                Main.Owner = this;
                this.Hide();
                Main.ShowDialog();
            } else { MessageBox.Show("Hibás felhasználónév vagy jelszó", "Hiba!", MessageBoxButton.OK); }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
