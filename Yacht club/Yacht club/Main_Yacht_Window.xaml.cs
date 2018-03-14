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

        private void DockPanel_MouseLeftButtonDown_Setting(object sender, MouseButtonEventArgs e)
        {
            ccWindow_2.Content = new ucBeallitasok();
        }
    }
}
