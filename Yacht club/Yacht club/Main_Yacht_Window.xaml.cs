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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Main_Yacht_Window : Window
    {
        public Main_Yacht_Window()
        {
            InitializeComponent();
        }

        private void miFoold_Click(object sender, RoutedEventArgs e)
        {
            stLogin.Visibility = Visibility.Hidden;
            stMenu_2.Visibility = Visibility.Visible;
            ccWindow_2.Content = new ucKijelzo_1();
            ccWindow_1.Content = new ucErtesitesek();
        }
        private void DockPanel_MouseLeftButtonDown_Exit(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }

        private void miKilepes_Click(object sender, RoutedEventArgs e)
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

        private void miBeallitasok_Click(object sender, RoutedEventArgs e)
        {
            if (ccWindow_2.Content == null || !(ccWindow_2.Content is ucBeallitasok))
            {
                ccWindow_2.Content = new ucBeallitasok();
            }
        }

        private void DockPanel_MouseLeftButtonDown_Yachts(object sender, MouseButtonEventArgs e)
        {
            stMenu_2_yacht.Visibility = Visibility.Visible;
            stMenu_2.Visibility = Visibility.Hidden;
            if (ccWindow_2.Content == null || !(ccWindow_2.Content is ucYachtok))
            {
                ccWindow_2.Content = new ucYachtok();
            }
        }

        private void DockPanel_MouseLeftButtonDown_Back(object sender, MouseButtonEventArgs e)
        {
            stMenu_2.Visibility = Visibility.Visible;
            stMenu_2_yacht.Visibility = Visibility.Hidden;
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

        private void DockPanel_MouseLeftButtonDown_Yacht_delete(object sender, MouseButtonEventArgs e)
        {
            if (ccWindow_2.Content == null || !(ccWindow_2.Content is ucYacht_delete))
            {
                ccWindow_2.Content = new ucYacht_delete();
            }
        }

        private void miYachts_Click(object sender, RoutedEventArgs e)
        {
            if (ccWindow_2.Content == null || !(ccWindow_2.Content is ucYachtok))
            {
                ccWindow_2.Content = new ucYachtok();
            }
        }

        private void miYachtAdd_Click(object sender, RoutedEventArgs e)
        {
            if (ccWindow_2.Content == null || !(ccWindow_2.Content is ucYacht_add))
            {
                ccWindow_2.Content = new ucYacht_add();
            }
        }

        private void miYachtRemove_Click(object sender, RoutedEventArgs e)
        {
            if (ccWindow_2.Content == null || !(ccWindow_2.Content is ucYacht_delete))
            {
                ccWindow_2.Content = new ucYacht_delete();
            }
        }
    }
}
