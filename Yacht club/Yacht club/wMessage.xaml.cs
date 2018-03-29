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
using Yacht_club.UsingControls;

namespace Yacht_club
{
    /// <summary>
    /// Interaction logic for wMessage.xaml
    /// </summary>
    public partial class wMessage : Window
    {
        private int action, id;
        private wLogin Login;
        Database.MysqlYacht dataYacht;
        Database.MysqlDevice dataDevice;
        private void Themes_Loading(object sender, RoutedEventArgs e)
        {
            DataContext = Globals.MainTheme;
        }
        public wMessage(int action, int id)
        {
            InitializeComponent();
            this.action = action;
            this.id = id;
            Loading();
        }

        private void Loading()
        {
            switch (action)
            {
                ///Kilépés
                case 1:
                    lbTitle.Content = "Kilépés";
                    lbText.Content = "Biztos kilép?";
                    break;
                ///Kijelentkezés
                case 2:
                    lbTitle.Content = "Kijelentkezés";
                    lbText.Content = "Biztos kijelentkezik?";
                    break;
                ///Yacht törlés
                case 3:
                    lbTitle.Content = "Yacht törlés";
                    lbText.Content = "Biztos törölni szeretné?";
                    break;
                ///Szállitoó törlés
                case 4:
                    lbTitle.Content = "Szállitóeszköz törlés";
                    lbText.Content = "Biztos törölni szeretnéd?";
                    break;
            }
        }

        private void btOK_Click(object sender, RoutedEventArgs e)
        {
            switch (action)
            {
                ///Kilépés
                case 1:
                    Application.Current.Shutdown();
                    break;
                ///Kijelentkezés
                case 2:
                    Login = new wLogin();
                    Globals.log_windows.delete();
                    Globals.User = null;
                    this.Hide();
                    Globals.Main.Hide();
                    Login.ShowDialog();
                    break;
                ///Yacht törlés
                case 3:
                    try
                    {
                        dataYacht = new Database.MysqlYacht();
                        dataYacht.MysqlDeleteYacht(id);
                        Globals.Main.ccWindow_2.Content = new ucYacht_delete();
                        Globals.log = "Törlés Sikeres! <Yach>";
                    }
                    catch (Exception)
                    {
                        Globals.log = "Törlés Sikertelen! <Yach>";
                    }
                    finally
                    {
                        Globals.Main.MainWindow.Opacity = 1;
                        Globals.Main.logAdd(false);
                        this.Hide();
                    }
                    break;
                ///Szállitoó törlés
                case 4:
                    try
                    {
                        dataDevice = new Database.MysqlDevice();
                        dataDevice.MysqlDeleteDevice(id);
                        Globals.Main.ccWindow_2.Content = new ucSzallito_delete();
                        Globals.log = "Törlés Sikeres! <Szállitóeszköz>";
                    }
                    catch (Exception)
                    {
                        Globals.log = "Törlés Sikertelen! <Szállitóeszköz>";
                    }
                    finally
                    {
                        Globals.Main.MainWindow.Opacity = 1;
                        Globals.Main.logAdd(false);
                        this.Hide();
                    }
                    break;
            }
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            Globals.Main.MainWindow.Opacity = 1;
            Globals.Main.logAdd(false);
            this.Hide();
        }
    }
}
