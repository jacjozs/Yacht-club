using System;
using System.Windows;
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
        Database.MysqlMessage dataMessage;
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
                ///Szállitó törlés
                case 4:
                    lbTitle.Content = "Szállitóeszköz törlés";
                    lbText.Content = "Biztos törölni szeretnéd?";
                    break;
                ///Üzenet törlés
                case 5:
                    lbTitle.Content = "Üzenet törlés";
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
                        Globals.Main.ccWindow_Main.Content = new ucYacht_delete();
                        Globals.log = "Törlés Sikeres! <Yach>";
                    }
                    catch (Exception)
                    {
                        Globals.log = "Törlés Sikertelen! <Yach>";
                    }
                    finally
                    {
                        Globals.Main.MainWindow.Opacity = 1;
                        Globals.Main.logAdd(true);
                        this.Hide();
                    }
                    break;
                ///Szállitó törlés
                case 4:
                    try
                    {
                        dataDevice = new Database.MysqlDevice();
                        dataDevice.MysqlDeleteDevice(id);
                        Globals.Main.ccWindow_Main.Content = new ucSzallito_delete();
                        Globals.log = "Törlés Sikeres! <Szállitóeszköz>";
                    }
                    catch (Exception)
                    {
                        Globals.log = "Törlés Sikertelen! <Szállitóeszköz>";
                    }
                    finally
                    {
                        Globals.Main.MainWindow.Opacity = 1;
                        Globals.Main.logAdd(true);
                        this.Hide();
                    }
                    break;
                ///Üzenet törlés
                case 5:
                    try
                    {
                        dataMessage = new Database.MysqlMessage();
                        dataMessage.MysqlMessageDelete(id);
                        Globals.Main.ccWindow_Main.Content = new ucUzenetek();
                        Globals.log = "Törlés Sikeres! <Üzenet>";
                    }
                    catch (Exception)
                    {
                        Globals.log = "Törlés Sikertelen! <Üzenet>";
                    }
                    finally
                    {
                        Globals.Main.MainWindow.Opacity = 1;
                        Globals.Main.logAdd(true);
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
