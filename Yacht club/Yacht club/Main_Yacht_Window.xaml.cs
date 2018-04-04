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
            ccWindow_2.Content = new ucKijelzo_1();
        }
        /// <summary>
        /// Log bejegyzés hozzáadása az egyes usercontrolhoz
        /// </summary>
        /// <param name="IsLog"></param>
        public void logAdd(bool IsLog)
        {
            if (IsLog)
            {
                Globals.log_windows.Add();
            }
            ccWindow_1.Content = Globals.log_windows;
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
                    if (ccWindow_2.Content == null || !(ccWindow_2.Content is ucKijelzo_1))
                    { ccWindow_2.Content = new ucKijelzo_1(); }
                    break;
                case "dpFelhasznalo":
                    stMenu_2_user.Visibility = Visibility.Visible;
                    cs_Menu_2.Visibility = Visibility.Hidden;
                    if (ccWindow_2.Content == null || !(ccWindow_2.Content is ucFelhasznalo))
                    {
                        ucFelhasznalo userFelhasznalo = new ucFelhasznalo();
                        ccWindow_2.Content = userFelhasznalo;
                        userFelhasznalo.ProfilImgLoad();
                    }
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
                    }
                    if (Globals.User.login.admin)
                    {
                        if (ccWindow_2.Content == null || !(ccWindow_2.Content is ucYachtok_all))
                        { ccWindow_2.Content = new ucYachtok_all(); }
                    }
                    else
                    {
                        if (ccWindow_2.Content == null || !(ccWindow_2.Content is ucYachtok))
                        { ccWindow_2.Content = new ucYachtok(); }
                    }
                    break;
                case "dpSzallitok":
                    if (Globals.User.login.admin)
                    {
                        stMenu_2_szallito.Visibility = Visibility.Visible;
                        cs_Menu_2.Visibility = Visibility.Hidden;
                    }
                    if (Globals.User.login.admin)
                    {
                        if (ccWindow_2.Content == null || !(ccWindow_2.Content is ucSzallitok_all))
                        { ccWindow_2.Content = new ucSzallitok_all(); }
                    }
                    else
                    {
                        if (ccWindow_2.Content == null || !(ccWindow_2.Content is ucSzallitok))
                        { ccWindow_2.Content = new ucSzallitok(); }
                    }
                    break;
                case "dpUzenetek":
                    if (ccWindow_2.Content == null || !(ccWindow_2.Content is ucUzenetek))
                    { ccWindow_2.Content = new ucUzenetek(); }
                    break;
                case "dpBeallitasok":
                    if (ccWindow_2.Content == null || !(ccWindow_2.Content is ucBeallitasok))
                    {
                        ucBeallitasok userSetting = new ucBeallitasok();
                        ccWindow_2.Content = userSetting;
                        userSetting.logining();
                    }
                    break;
                case "dpInfo":
                    if (ccWindow_2.Content == null || !(ccWindow_2.Content is ucInfok))
                    { ccWindow_2.Content = new ucInfok(); }
                    break;
                case "dpVissza1":
                case "dpVissza2":
                case "dpVissza3":
                    cs_Menu_2.Visibility = Visibility.Visible;
                    if (stMenu_2_user.Visibility == Visibility.Visible)
                    { stMenu_2_user.Visibility = Visibility.Hidden; }
                    if (stMenu_2_yacht.Visibility == Visibility.Visible)
                    { stMenu_2_yacht.Visibility = Visibility.Hidden; }
                    if (stMenu_2_szallito.Visibility == Visibility.Visible)
                    { stMenu_2_szallito.Visibility = Visibility.Hidden; }
                    if (ccWindow_2.Content == null || !(ccWindow_2.Content is ucKijelzo_1))
                    { ccWindow_2.Content = new ucKijelzo_1(); }
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
                default:
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
                    {
                        if (ccWindow_2.Content == null || !(ccWindow_2.Content is ucYachtok_all))
                        { ccWindow_2.Content = new ucYachtok_all(); }
                    }
                    else
                    {
                        if (ccWindow_2.Content == null || !(ccWindow_2.Content is ucYachtok))
                        { ccWindow_2.Content = new ucYachtok(); }
                    }
                    break;
                case "lbYachtok_add":
                    if (ccWindow_2.Content == null || !(ccWindow_2.Content is ucYacht_add))
                    { ccWindow_2.Content = new ucYacht_add(); }
                    break;
                case "lbYachtok_delete":
                    if (ccWindow_2.Content == null || !(ccWindow_2.Content is ucYacht_delete))
                    { ccWindow_2.Content = new ucYacht_delete(); }
                    break;
                case "lbSzallitok":
                    if (Globals.User.login.admin)
                    {
                        if (ccWindow_2.Content == null || !(ccWindow_2.Content is ucSzallitok_all))
                        { ccWindow_2.Content = new ucSzallitok_all(); }
                    }
                    else
                    {
                        if (ccWindow_2.Content == null || !(ccWindow_2.Content is ucSzallitok))
                        { ccWindow_2.Content = new ucSzallitok(); }
                    }
                    break;
                case "lbSzallitok_add":
                    if (ccWindow_2.Content == null || !(ccWindow_2.Content is ucSzallito_add))
                    { ccWindow_2.Content = new ucSzallito_add(); }
                    break;
                case "lbSzallitok_delete":
                    if (ccWindow_2.Content == null || !(ccWindow_2.Content is ucSzallito_delete))
                    { ccWindow_2.Content = new ucSzallito_delete(); }
                    break;
                case "lbYacht_berles":
                    if (ccWindow_2.Content == null || !(ccWindow_2.Content is ucYacht_berles))
                    { ccWindow_2.Content = new ucYacht_berles(); }
                    break;
                case "lbSzalliteszkoz_berles":
                    if (ccWindow_2.Content == null || !(ccWindow_2.Content is ucSzallito_berles))
                    { ccWindow_2.Content = new ucSzallito_berles(); }
                    break;
                case "lbBerbeadott":
                    if (ccWindow_2.Content == null || !(ccWindow_2.Content is ucBerbeadott))
                    { ccWindow_2.Content = new ucBerbeadott(); }
                    break;
                case "lbKimutatas":
                    if (ccWindow_2.Content == null || !(ccWindow_2.Content is ucKimutatasok))
                    { ccWindow_2.Content = new ucKimutatasok(); }
                    break;
                default:
                    break;
            }
        }
    }
}
