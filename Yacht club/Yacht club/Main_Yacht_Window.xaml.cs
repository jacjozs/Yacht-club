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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Yacht_club.UsingControls;
using System.Threading;

namespace Yacht_club
{
    /// <summary>
    /// Interaction logic for Main_Yacht_Window.xaml
    /// </summary>
    public partial class Main_Yacht_Window : Window
    {
        ///Ne töröld ki mert ez a kettő nélkül nem megy
        ///Ha tudsz rá jó megoldást alkalmazd (wLogin.xaml.cs-ben van a másik része!)
        private Felhasznalo user;
        internal Felhasznalo User { get => user; set => user = value; }

        private wLogin Login;
        public Main_Yacht_Window()
        {
            InitializeComponent();
            User = new Felhasznalo();
            Logining();
        }
        /// <summary>
        /// Ez azért kell hogy amikor a login ablak végéez akkor eltünjön
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown();
        }
        private void DockPanel_MouseLeftButtonDown_Logout(object sender, MouseButtonEventArgs e)
        {
            Login = new wLogin();
            Login.Owner = this;
            this.Hide();
            Login.ShowDialog();
        }

        private void Logining()
        {
            cs_Menu_2.Visibility = Visibility.Visible;
            dpUdvezles.Visibility = Visibility.Visible;
            if (ccWindow_2.Content == null || !(ccWindow_2.Content is ucKijelzo_1))
            {
                ccWindow_2.Content = new ucKijelzo_1();
            }
            if (ccWindow_1.Content == null || !(ccWindow_1.Content is ucErtesitesek))
            {
                ccWindow_1.Content = new ucErtesitesek();
            }
        }
        private void DockPanel_MouseLeftButtonDown_Foold(object sender, MouseButtonEventArgs e)
        {
            if (ccWindow_2.Content == null || !(ccWindow_2.Content is ucKijelzo_1))
            {
                ccWindow_2.Content = new ucKijelzo_1();
            }
            if (ccWindow_1.Content == null || !(ccWindow_1.Content is ucErtesitesek))
            {
                ccWindow_1.Content = new ucErtesitesek();
            }
        }
        private void DockPanel_MouseLeftButtonDown_Exit(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }

        private void DockPanel_MouseLeftButtonDown_Setting(object sender, MouseButtonEventArgs e)
        {
            if (ccWindow_2.Content == null || !(ccWindow_2.Content is ucBeallitasok))
            {
                ccWindow_2.Content = new ucBeallitasok();
            }
        }

        private void DockPanel_MouseLeftButtonDown_Yachts(object sender, MouseButtonEventArgs e)
        {
            stMenu_2_yacht.Visibility = Visibility.Visible;
            cs_Menu_2.Visibility = Visibility.Hidden;
            if (ccWindow_2.Content == null || !(ccWindow_2.Content is ucYachtok))
            {
                ccWindow_2.Content = new ucYachtok();
            }
        }

        private void DockPanel_MouseLeftButtonDown_Szallitok(object sender, MouseButtonEventArgs e)
        {
            stMenu_2_szallito.Visibility = Visibility.Visible;
            cs_Menu_2.Visibility = Visibility.Hidden;
            if (ccWindow_2.Content == null || !(ccWindow_2.Content is ucSzallitok))
            {
                ccWindow_2.Content = new ucSzallitok();
            }
        }

        private void DockPanel_MouseLeftButtonDown_Back(object sender, MouseButtonEventArgs e)
        {
            cs_Menu_2.Visibility = Visibility.Visible;
            if (stMenu_2_user.Visibility == Visibility.Visible)
            {
                stMenu_2_user.Visibility = Visibility.Hidden;
            }
            if (stMenu_2_yacht.Visibility == Visibility.Visible)
            {
                stMenu_2_yacht.Visibility = Visibility.Hidden;
            }
            if (stMenu_2_szallito.Visibility == Visibility.Visible)
            {
                stMenu_2_szallito.Visibility = Visibility.Hidden;
            }
            if (ccWindow_2.Content == null || !(ccWindow_2.Content is ucKijelzo_1))
            {
                ccWindow_2.Content = new ucKijelzo_1();
            }
        }

        private void DockPanel_MouseLeftButtonDown_Yacht_add(object sender, MouseButtonEventArgs e)
        {
            if (ccWindow_2.Content == null || !(ccWindow_2.Content is ucYacht_add))
            {
                ccWindow_2.Content = new ucYacht_add();
            }
        }
        private void DockPanel_MouseLeftButtonDown_Szallito_add(object sender, MouseButtonEventArgs e)
        {
            if (ccWindow_2.Content == null || !(ccWindow_2.Content is ucSzallito_add))
            {
                ccWindow_2.Content = new ucSzallito_add();
            }
        }

        private void DockPanel_MouseLeftButtonDown_Yacht_delete(object sender, MouseButtonEventArgs e)
        {
            if (ccWindow_2.Content == null || !(ccWindow_2.Content is ucYacht_delete))
            {
                ccWindow_2.Content = new ucYacht_delete();
            }
        }
        private void DockPanel_MouseLeftButtonDown_Szallito_delete(object sender, MouseButtonEventArgs e)
        {
            if (ccWindow_2.Content == null || !(ccWindow_2.Content is ucSzallito_delete))
            {
                ccWindow_2.Content = new ucSzallito_delete();
            }
        }

        private void DockPanel_MouseLeftButtonDown_Info(object sender, MouseButtonEventArgs e)
        {
            if (ccWindow_2.Content == null || !(ccWindow_2.Content is ucInfok))
            {
                ccWindow_2.Content = new ucInfok();
            }
        }

        private void DockPanel_MouseLeftButtonDown_User(object sender, MouseButtonEventArgs e)
        {
            stMenu_2_user.Visibility = Visibility.Visible;
            cs_Menu_2.Visibility = Visibility.Hidden;
        }
    }
}
