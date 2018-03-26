using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Yacht_club.UsingControls
{
    /// <summary>
    /// Interaction logic for ucYacht_add.xaml
    /// </summary>
    public partial class ucYacht_add : UserControl
    {
        private Yacht newYacht;
        private string filepath = "";
        private Dictionary<string, int> list;
        private Yacht_club.Database.MysqlYacht data;
        public ucYacht_add()
        {
            InitializeComponent();
            login_name();
        }

        private void bt_Hozza_add(object sender, RoutedEventArgs e)
        {
            try
            {
                newYacht = new Yacht();
                newYacht.nev = tbYacht_nev.Text;
                newYacht.tipus = tbYacht_tipus.Text;
                newYacht.login_id = int.Parse(list[cbYacht_tulaj.Text].ToString());
                newYacht.ferohely = int.Parse(tbYacht_ferohely.Text);
                newYacht.suly = int.Parse(tbYacht_tomeg.Text);
                newYacht.szeles = int.Parse(tbYacht_szeles.Text);
                newYacht.hossz = int.Parse(tbYacht_hossz.Text);
                newYacht.magas = int.Parse(tbYacht_magas.Text);
                newYacht.kep = System.Drawing.Image.FromFile(filepath);
                if (data.MysqlRegisztracionYacht(newYacht))
                    AddYachtLog(newYacht.nev + " Hozzáadva!");
                else Globals.log = "Hozzáadás Sikertelen! <Yacht>";
            }
            catch (Exception)
            {
                Globals.log = "Hozzáadás Sikertelen! <Yacht>";
            }
            finally
            {
                Globals.log = "Hozzáadás Sikeres! <Yacht>";
            }
            Globals.Main.logAdd(true);
        }

        private void bt_Yacht_img_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.InitialDirectory = "C:\\";
            open.Filter = "Image Files(*.PNG;*.JPG;*.JPEG;*.BMP;*.GIF)|*.PNG;*.JPG;*.JPEG;*.BMP;*.GIF";
            open.FilterIndex = 1;
            Nullable<bool> dialogok = open.ShowDialog();

            if (dialogok == true)
            {
                filepath = open.FileName;
                tbYacht_image.Text = filepath;
            }
        }

        public void login_name()
        {
            data = new Yacht_club.Database.MysqlYacht();
            list = data.MysqlRegisztracionYachtLoginName();
            cbYacht_tulaj.ItemsSource = list.Keys;
        }

        private void AddYachtLog(string log)
        {
            lbYachtLog.Items.Insert(0, log);
        }
    }
}
