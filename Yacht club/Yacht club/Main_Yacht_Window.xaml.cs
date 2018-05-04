using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Yacht_club.UsingControls;
using Yacht_club.UsingControls.Admin;

namespace Yacht_club
{
    /// <summary>
    /// Interaction logic for Main_Yacht_Window.xaml
    /// </summary>
    public partial class Main_Yacht_Window : Window
    {
        /// <summary>
        /// Ablakok meghívásához szukséges objektum példányok
        /// </summary>
        private wRegistration Register;
        private wMessage Message;
        /// <summary>
        /// Téma betöltése
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Themes_Loading(object sender, RoutedEventArgs e)
        {
            UpdateTheme();
        }
        /// <summary>
        /// Téma újratöltése a DataContext-be
        /// </summary>
        public void UpdateTheme()
        {
            DataContext = Globals.MainTheme;
        }
        public Main_Yacht_Window()
        {
            InitializeComponent();
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
        /// <summary>
        /// belépési ablakok elhelyezése
        /// </summary>
        private void Logining()
        {
            cs_Menu_2.Visibility = Visibility.Visible;
            dpUdvezles.Visibility = Visibility.Visible;
            ccWindow_Main.Content = new ucKijelzo_1();
        }
        /// <summary>
        /// Log bejegyzés hozzáadása az egyes usercontrolhoz
        /// </summary>
        /// <param name="IsLog"></param>
        public void logAdd(bool IsLog)
        {
            if (IsLog)
                Globals.log_windows.Add();
            ccWindow_Log.Content = Globals.log_windows;
        }
        /// <summary>
        /// Főmenük választása
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dpMouse_Click(object sender, MouseButtonEventArgs e)
        {
            DockPanel gomb = (DockPanel)sender;
            switch (gomb.Name.ToString())
            {
                case "dpFomenu":
                    if (ccWindow_Main.Content == null || !(ccWindow_Main.Content is ucKijelzo_1))
                        ccWindow_Main.Content = new ucKijelzo_1();
                    break;
                case "dpFelhasznalo":
                    stMenu_2_user.Visibility = Visibility.Visible;
                    cs_Menu_2.Visibility = Visibility.Hidden;
                    if (ccWindow_Main.Content == null || !(ccWindow_Main.Content is ucFelhasznalo))
                        ccWindow_Main.Content = new ucFelhasznalo(0);
                    break;
                case "dpKijelentkezes":
                    Message = new wMessage(2, 0);
                    Globals.Main.Opacity = 0.6;
                    Message.ShowDialog();
                    break;
                case "dpYaktok":
                    if (Globals.User.login.admin)
                    {
                        stMenu_2_yacht.Visibility = Visibility.Visible;
                        cs_Menu_2.Visibility = Visibility.Hidden;
                        if (ccWindow_Main.Content == null || !(ccWindow_Main.Content is ucYachtok_all))
                            ccWindow_Main.Content = new ucYachtok_all();
                    }
                    else
                    {
                        if (ccWindow_Main.Content == null || !(ccWindow_Main.Content is ucYachtok))
                            ccWindow_Main.Content = new ucYachtok();
                    }
                    break;
                case "dpSzallitok":
                    if (Globals.User.login.admin)
                    {
                        stMenu_2_szallito.Visibility = Visibility.Visible;
                        cs_Menu_2.Visibility = Visibility.Hidden;
                        if (ccWindow_Main.Content == null || !(ccWindow_Main.Content is ucSzallitok_all))
                            ccWindow_Main.Content = new ucSzallitok_all();
                    }
                    else
                    {
                        if (ccWindow_Main.Content == null || !(ccWindow_Main.Content is ucSzallitok))
                            ccWindow_Main.Content = new ucSzallitok();
                    }
                    break;
                case "dpUzenetek":
                    if (ccWindow_Main.Content == null || !(ccWindow_Main.Content is ucUzenetek))
                        ccWindow_Main.Content = new ucUzenetek();
                    break;
                case "dpBeallitasok":
                    if (ccWindow_Main.Content == null || !(ccWindow_Main.Content is ucBeallitasok))
                        ccWindow_Main.Content = new ucBeallitasok();
                    break;
                case "dpInfo":
                    if (ccWindow_Main.Content == null || !(ccWindow_Main.Content is ucInfok))
                        ccWindow_Main.Content = new ucInfok();
                    break;
                case "dpVissza1":
                case "dpVissza2":
                case "dpVissza3":
                    cs_Menu_2.Visibility = Visibility.Visible;
                    if (stMenu_2_user.IsVisible)
                    { stMenu_2_user.Visibility = Visibility.Hidden; }
                    if (stMenu_2_yacht.IsVisible)
                    { stMenu_2_yacht.Visibility = Visibility.Hidden; }
                    if (stMenu_2_szallito.IsVisible)
                    { stMenu_2_szallito.Visibility = Visibility.Hidden; }
                    if (ccWindow_Main.Content == null || !(ccWindow_Main.Content is ucKijelzo_1))
                        ccWindow_Main.Content = new ucKijelzo_1();
                    break;
                case "dpRegist":
                    Register = new wRegistration();
                    MainWindow.Opacity = 0.6;
                    Register.ShowDialog();
                    break;
                case "dpKilepes":
                    Message = new wMessage(1, 0);
                    Globals.Main.Opacity = 0.6;
                    Message.ShowDialog();
                    break;
            }
        }
        /// <summary>
        /// Almenük választása
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbMouse_Click(object sender, MouseButtonEventArgs e)
        {
            Label gomb = (Label)sender;
            switch (gomb.Name.ToString())
            {
                case "lbYachtok":
                    if (Globals.User.login.admin)
                        if (ccWindow_Main.Content == null || !(ccWindow_Main.Content is ucYachtok_all))
                            ccWindow_Main.Content = new ucYachtok_all();
                    else
                        if (ccWindow_Main.Content == null || !(ccWindow_Main.Content is ucYachtok))
                            ccWindow_Main.Content = new ucYachtok();
                    break;
                case "lbYachtok_add":
                    if (ccWindow_Main.Content == null || !(ccWindow_Main.Content is ucYacht_add))
                        ccWindow_Main.Content = new ucYacht_add();
                    break;
                case "lbYachtok_delete":
                    if (ccWindow_Main.Content == null || !(ccWindow_Main.Content is ucYacht_delete))
                        ccWindow_Main.Content = new ucYacht_delete();
                    break;
                case "lbSzallitok":
                    if (Globals.User.login.admin)
                        if (ccWindow_Main.Content == null || !(ccWindow_Main.Content is ucSzallitok_all))
                            ccWindow_Main.Content = new ucSzallitok_all();
                    else
                        if (ccWindow_Main.Content == null || !(ccWindow_Main.Content is ucSzallitok))
                            ccWindow_Main.Content = new ucSzallitok();
                    break;
                case "lbSzallitok_add":
                    if (ccWindow_Main.Content == null || !(ccWindow_Main.Content is ucSzallito_add))
                        ccWindow_Main.Content = new ucSzallito_add();
                    break;
                case "lbSzallitok_delete":
                    if (ccWindow_Main.Content == null || !(ccWindow_Main.Content is ucSzallito_delete))
                        ccWindow_Main.Content = new ucSzallito_delete();
                    break;
                case "lbYacht_berles":
                    if (ccWindow_Main.Content == null || !(ccWindow_Main.Content is ucYacht_berles))
                        ccWindow_Main.Content = new ucYacht_berles();
                    break;
                case "lbSzalliteszkoz_berles":
                    if (ccWindow_Main.Content == null || !(ccWindow_Main.Content is ucSzallito_berles))
                        ccWindow_Main.Content = new ucSzallito_berles();
                    break;
                case "lbBerbeadott":
                    if (ccWindow_Main.Content == null || !(ccWindow_Main.Content is ucBerbeadott))
                        ccWindow_Main.Content = new ucBerbeadott();
                    break;
                case "lbKimutatas":
                    if (ccWindow_Main.Content == null || !(ccWindow_Main.Content is ucKimutatasok))
                        ccWindow_Main.Content = new ucKimutatasok();
                    break;
            }
        }
        /// <summary>
        /// Backspace gomb hatására egy ablakot vissza ugrik
        /// Csak egyet!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Back:
                    ccWindow_Main.Content = Globals.History;
                    break;
                case Key.Escape:
                    Message = new wMessage(1, 0);
                    Globals.Main.Opacity = 0.6;
                    Message.ShowDialog();
                    break;
            }
        }
        /// <summary>
        /// Ablak mozgatás
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Moveing_Click(object sender, MouseButtonEventArgs e)
        {
            if (Globals.Main.IsMouseOver)
                DragMove();
        }
    }
}
