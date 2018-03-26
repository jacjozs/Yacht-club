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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace Yacht_club.UsingControls
{
    /// <summary>
    /// Interaction logic for ucBeallitasok.xaml
    /// </summary>
    public partial class ucBeallitasok : UserControl
    {
        private string filepath = "";
        private Felhasznalo user;

        private Database data;
        public ucBeallitasok()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Kép tallozás és az elérésiút mentése valamint kiírása a textbox-ba
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btkep_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.InitialDirectory = "C:\\";
            open.Filter = "Image Files(*.PNG;*.JPG;*.JPEG;*.BMP;*.GIF)|*.PNG;*.JPG;*.JPEG;*.BMP;*.GIF";
            open.FilterIndex = 1;
            Nullable<bool> dialogok = open.ShowDialog();

            if (dialogok == true)
            {
                filepath = open.FileName;
                tbKep.Text = filepath;
            }
        }
        /// <summary>
        /// Elküldi az adatbázis interface-nek a kész felhasználó objectumot a frisitésre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btAlkalmaz_Click(object sender, RoutedEventArgs e)
        {
            user = new Felhasznalo();
            try
            {
                user = Globals.User;
                user.login = Globals.User.login;
                if (tbNickName.Text != "") user.nickname = tbNickName.Text;
                if (tbFirstName.Text != "") user.veztek_nev = tbFirstName.Text;
                if (tbLastName.Text != "") user.kereszt_nev = tbLastName.Text;
                if (tbVaros.Text != "") user.varos = tbVaros.Text;
                if (tbIranyitoszm.Text != "") user.iranyitoszm = int.Parse(tbIranyitoszm.Text);
                if (tbLakcim.Text != "") user.lakcim = tbLakcim.Text;
                if (tbOrszag.Text != "") user.orszag = tbOrszag.Text;
                if (pbPasswd.Password != "" && passwdEllenorzes(pbOldPasswd.Password, pbPasswd.Password, pbRePasswd.Password)) user.login.jelszo = pbPasswd.Password;
                if (tbEmail.Text != "") user.login.email = tbEmail.Text;
                if (filepath != "") user.kep = System.Drawing.Image.FromFile(filepath);

                data = new Database();
                if (user != null) data.MysqlUpdateUser(user);
            }
            catch (Exception)
            {
                Globals.log = "Módosítás Sikertelen! <Beállitások>";
            }
            finally
            {
                Globals.User = user;
                Globals.log = "Módosítás Sikeres! <Beállitások>";
                Globals.Main.lbNickname.Content = user.nickname;
            }
            Globals.Main.logAdd(true);
        }

        private bool passwdEllenorzes(string regi, string uj, string reUj)
        {
            if (regi == Globals.User.login.jelszo && uj == reUj) return true;
            return false;
        }

        public void logining()
        {
            tbNickName.Text = Globals.User.nickname;
            tbFirstName.Text = Globals.User.veztek_nev;
            tbLastName.Text = Globals.User.kereszt_nev;
            tbVaros.Text = Globals.User.varos;
            tbIranyitoszm.Text = Globals.User.iranyitoszm.ToString();
            tbLakcim.Text = Globals.User.lakcim;
            tbOrszag.Text = Globals.User.orszag;
            tbEmail.Text = Globals.User.login.email;
        }
    }
}
