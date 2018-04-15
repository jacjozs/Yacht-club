using System.Windows.Controls;
using System.Windows;
using Yacht_club.Database;

namespace Yacht_club.UsingControls
{
    /// <summary>
    /// Interaction logic for ucUzenet.xaml
    /// </summary>
    public partial class ucUzenet : UserControl
    {
        private MysqlMessage data;
        public ucUzenet()
        {
            InitializeComponent();
            Globals.UpdateHistory();
            Loading();
        }

        private void Loading()
        {
            lbFelado.Content = Globals.selectedMessage.felado_nev;
            lbDatum.Content = Globals.selectedMessage.keletkezett.ToString();
            if (Globals.selectedMessage.yacht_id != 0)
            {
                lbYNev.Content = Globals.selectedMessage.yacht_nev;
                lbYachtName.Visibility = Visibility.Visible;
                lbIndul.Content = Globals.selectedMessage.honnan_id;
                lbErkez.Content = Globals.selectedMessage.hova_id;
            }
            else
            {
                if (Globals.selectedMessage.device_id != 0)
                {
                    lbSzId.Content = Globals.selectedMessage.device_id;
                    lbSzallitoName.Visibility = Visibility.Visible;
                    lbErkez.Visibility = Visibility.Hidden;
                    lbIndul.Visibility = Visibility.Hidden;
                    lberkezes.Visibility = Visibility.Hidden;
                    lbindulas.Visibility = Visibility.Hidden;
                }
            }
            lbMettol.Content = Globals.selectedMessage.kezdete.ToString();
            lbMeddig.Content = Globals.selectedMessage.vege.ToString();

            if (Globals.selectedMessage.BlVissza || !Globals.selectedMessage.NEWbl)
            {
                btElfofad.Visibility = Visibility.Hidden;
                btElutasit.Visibility = Visibility.Hidden;
                lbFogadUtasit.Visibility = Visibility.Visible;
                lbFogadUtasit.Content = Globals.selectedMessage.elfogadvastr;
            }
            if (!Globals.selectedMessage.BlVissza && Globals.selectedMessage.felado_id == Globals.User.member_id)
            {
                btElfofad.Visibility = Visibility.Hidden;
                btElutasit.Visibility = Visibility.Hidden;
                lbFogadUtasit.Visibility = Visibility.Visible;
                lbFogadUtasit.Content = "Még nem érkezet válasz!";
            }
        }

        private void lbFelado_click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Globals.Main.ccWindow_Main.Content = new ucFelhasznalo(Globals.selectedMessage.felado_id);
        }

        private void lbYNev_click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Globals.Main.ccWindow_Main.Content = new ucYacht(Globals.selectedMessage.yacht_id);
        }

        private void lbSzId_click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Globals.Main.ccWindow_Main.Content = new ucSzallito(Globals.selectedMessage.device_id);
        }

        private void btElfofad_Click(object sender, RoutedEventArgs e)
        {
            data = new Database.MysqlMessage();
            data.MysqlMessageAcceptUpdate(1);
            Globals.selectedMessage.BlVissza = true;
            Globals.selectedMessage.elfogadvabl = true;
            Globals.selectedMessage.elfogadvastr = "Elfogadva!";
            Loading();
            if (Globals.selectedMessage.yacht_nev != null)
                Globals.log = "Elfogadta a \"" + Globals.selectedMessage.yacht_nev + "\" nevü Yacht bérlését!<Üzenetek>";
            else Globals.log = "Elfogadta a " + Globals.selectedMessage.device_id + " id-vel rendelkező Szállitóeszköz bérlését! <Üzenetek>";
            Globals.Main.logAdd(true);
        }

        private void btElutasit_Click(object sender, RoutedEventArgs e)
        {
            data = new Database.MysqlMessage();
            data.MysqlMessageAcceptUpdate(0);
            Globals.selectedMessage.BlVissza = true;
            Globals.selectedMessage.elfogadvabl = false;
            Globals.selectedMessage.elfogadvastr = "Elutasítva!";
            Loading();
            if (Globals.selectedMessage.yacht_nev != null)
                Globals.log = "Elutasítota a \"" + Globals.selectedMessage.yacht_nev + "\" nevü Yacht bérlését!<Üzenetek>";
            else Globals.log = "Elutasítota a " + Globals.selectedMessage.device_id + " id-vel rendelkező Szállitóeszköz bérlését! <Üzenetek>";
            Globals.Main.logAdd(true);
        }
    }
}
