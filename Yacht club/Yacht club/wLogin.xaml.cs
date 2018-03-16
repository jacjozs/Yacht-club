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

        private wRegistration Registration;
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
            Main = new Main_Yacht_Window();
            //A kikereset és ellenörzött adatok bevitele a main user változoba
            //Egyfajta átadás
            Main.User.admin = true;
            //...
            //A login ablak eltünéséhez szükséges
            Main.Owner = this;
            this.Hide();
            Main.ShowDialog();
        }

        private void btRegiszt_Click(object sender, RoutedEventArgs e)
        {
            Registration = new wRegistration();
            Registration.Owner = this;
            this.Hide();
            Registration.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
