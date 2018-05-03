using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Yacht_club.Database;
using Yacht_club.Moduls;

namespace Yacht_club.UsingControls
{
    /// <summary>
    /// Interaction logic for ucBerles.xaml
    /// </summary>
    public partial class ucBerles : UserControl
    {
        private MysqlMessage data;
        private Message uzenet;
        private int DevYacht;
        private Dictionary<int, string> list;

        public ucBerles(int DevYacht)
        {
            InitializeComponent();
            this.DevYacht = DevYacht;
            Loading(DevYacht);
            Globals.UpdateHistory();
        }

        private void Loading(int DevYacht)
        {
            data = new MysqlMessage();
            switch (DevYacht)
            {
                case 1:
                    YachtData();
                    list = MysqlGeneral.MysqlPortName();
                    foreach (var entry in list)
                    {
                        TbBerlesFrom.AddItem(new AutoCompleteEntry(entry.Key, entry.Value, Globals.cut(entry.Value)));
                        TbBerlesTo.AddItem(new AutoCompleteEntry(entry.Key, entry.Value, Globals.cut(entry.Value)));
                    }
                    break;
                case 2:
                    DeviceData();
                    TbBerlesFrom.Visibility = Visibility.Hidden;
                    TbBerlesTo.Visibility = Visibility.Hidden;
                    lbhonnan.Visibility = Visibility.Hidden;
                    lbhova.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void YachtData()
        {
            Style BerlesStackLabel = Application.Current.FindResource("BerlesStackLabel") as Style;
            StackPanel panel = new StackPanel();
            StackPanel panel2 = new StackPanel();

            Label nev = new Label();
            Label nev2 = new Label();
            nev.Content = "Név:";
            nev2.Content = Globals.selectedYacht.nev;
            nev.Style = BerlesStackLabel;
            nev2.Style = BerlesStackLabel;

            Label gyarto = new Label();
            Label gyarto2 = new Label();
            gyarto.Content = "Gyártó:";
            gyarto2.Content = Globals.selectedYacht.gyarto;
            gyarto.Style = BerlesStackLabel;
            gyarto2.Style = BerlesStackLabel;

            Label ferohely = new Label();
            Label ferohely2 = new Label();
            ferohely.Content = "Férőhelyek száma:";
            ferohely2.Content = Globals.selectedYacht.ferohely;
            ferohely.Style = BerlesStackLabel;
            ferohely2.Style = BerlesStackLabel;

            Label szeles = new Label();
            Label szeles2 = new Label();
            szeles.Content = "Szélesség(m):";
            szeles2.Content = Globals.selectedYacht.szeles;
            szeles.Style = BerlesStackLabel;
            szeles2.Style = BerlesStackLabel;

            Label hossz = new Label();
            Label hossz2 = new Label();
            hossz.Content = "Hosszúság(m):";
            hossz2.Content = Globals.selectedYacht.hossz;
            hossz.Style = BerlesStackLabel;
            hossz2.Style = BerlesStackLabel;

            Label merul = new Label();
            Label merul2 = new Label();
            merul.Content = "Merülés(m):";
            merul2.Content = Globals.selectedYacht.merules;
            merul.Style = BerlesStackLabel;
            merul2.Style = BerlesStackLabel;

            Label speed = new Label();
            Label speed2 = new Label();
            speed.Content = "Sebesség(csomó):";
            speed2.Content = Globals.selectedYacht.sebesseg;
            speed.Style = BerlesStackLabel;
            speed2.Style = BerlesStackLabel;

            Label kikoto = new Label();
            Label kikoto2 = new Label();
            kikoto.Content = "Jelenlegi helye:";
            kikoto2.Content = Globals.selectedYacht.kikoto;
            kikoto.Style = BerlesStackLabel;
            kikoto2.Style = BerlesStackLabel;

            Label napiar = new Label();
            Label napiar2 = new Label();
            napiar.Content = "Napi ár:";
            napiar2.Content = Globals.selectedYacht.napi_ar;
            napiar.Style = BerlesStackLabel;
            napiar2.Style = BerlesStackLabel;

            panel.Children.Add(nev);
            panel2.Children.Add(nev2);
            panel.Children.Add(gyarto);
            panel2.Children.Add(gyarto2);
            panel.Children.Add(ferohely);
            panel2.Children.Add(ferohely2);
            panel.Children.Add(szeles);
            panel2.Children.Add(szeles2);
            panel.Children.Add(hossz);
            panel2.Children.Add(hossz2);
            panel.Children.Add(merul);
            panel2.Children.Add(merul2);
            panel.Children.Add(speed);
            panel2.Children.Add(speed2);
            panel.Children.Add(kikoto);
            panel2.Children.Add(kikoto2);
            panel.Children.Add(napiar);
            panel2.Children.Add(napiar2);

            stYachtDeviceData.Children.Add(panel);
            stYachtDeviceData.Children.Add(panel2);
        }

        private void DeviceData()
        {
            Style BerlesStackLabel = Application.Current.FindResource("BerlesStackLabel") as Style;
            StackPanel panel = new StackPanel();
            StackPanel panel2 = new StackPanel();

            Label tipus = new Label();
            Label tipus2 = new Label();
            tipus.Content = "Tipus:";
            tipus2.Content = Globals.selectedDevice.tipus;
            tipus.Style = BerlesStackLabel;
            tipus2.Style = BerlesStackLabel;

            Label full_name = new Label();
            Label full_name2 = new Label();
            full_name.Content = "Tulajdonos Neve:";
            full_name2.Content = Globals.selectedDevice.full_name;
            full_name.Style = BerlesStackLabel;
            full_name2.Style = BerlesStackLabel;

            Label max_hossz = new Label();
            Label max_hossz2 = new Label();
            max_hossz.Content = "Max. Hosszúság:";
            max_hossz2.Content = Globals.selectedDevice.max_hossz;
            max_hossz.Style = BerlesStackLabel;
            max_hossz2.Style = BerlesStackLabel;

            Label max_suly = new Label();
            Label max_suly2 = new Label();
            max_suly.Content = "Max. Teherbírás:";
            max_suly2.Content = Globals.selectedDevice.max_suly;
            max_suly.Style = BerlesStackLabel;
            max_suly2.Style = BerlesStackLabel;

            Label napi_ar = new Label();
            Label napi_ar2 = new Label();
            napi_ar.Content = "Napi ár(EUR):";
            napi_ar2.Content = Globals.selectedDevice.napi_ar;
            napi_ar.Style = BerlesStackLabel;
            napi_ar2.Style = BerlesStackLabel;

            panel.Children.Add(tipus);
            panel2.Children.Add(tipus2);
            panel.Children.Add(full_name);
            panel2.Children.Add(full_name2);
            panel.Children.Add(max_suly);
            panel2.Children.Add(max_suly2);
            panel.Children.Add(max_hossz);
            panel2.Children.Add(max_hossz2);
            panel.Children.Add(napi_ar);
            panel2.Children.Add(napi_ar2);

            stYachtDeviceData.Children.Add(panel);
            stYachtDeviceData.Children.Add(panel2);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                uzenet = new Message();
                switch (DevYacht)
                {
                    case 1:
                        uzenet.cimzett_id = Globals.selectedYacht.member_id;
                        uzenet.yacht_id = Globals.selectedYacht.id;
                        uzenet.device_id = -1;
                        uzenet.price = dpEndDate.SelectedDate.Value.Subtract(dpStartDate.SelectedDate.Value).Days * Globals.selectedYacht.napi_ar;
                        uzenet.kezdete = dpStartDate.DisplayDate;
                        uzenet.vege = dpEndDate.DisplayDate;
                        uzenet.honnan_id = TbBerlesFrom.ID;
                        uzenet.hova_id = TbBerlesTo.ID;
                        break;
                    case 2:
                        uzenet.cimzett_id = Globals.selectedDevice.member_id;
                        uzenet.device_id = Globals.selectedDevice.id;
                        uzenet.yacht_id = -1;
                        uzenet.price = dpEndDate.SelectedDate.Value.Subtract(dpStartDate.SelectedDate.Value).Days * Globals.selectedDevice.napi_ar;
                        uzenet.kezdete = dpStartDate.DisplayDate;
                        uzenet.vege = dpEndDate.DisplayDate;
                        uzenet.honnan_id = -1;
                        uzenet.hova_id = -1;
                        break;
                }
                data.MysqlNewMessage(uzenet);
                Globals.log = "Sikeres bérlési kérelem! <Bérlések>";
            }
            catch (System.Exception)
            {
                Globals.log = "Sikertelen bérlési kérelem! <Bérlések>";
            }
            Globals.Main.logAdd(true);
        }

        private void dpEndDate_LostFocus(object sender, RoutedEventArgs e)
        {
            switch (DevYacht)
            {
                case 1:
                    lbcalkulPrice.Content = dpEndDate.SelectedDate.Value.Subtract(dpStartDate.SelectedDate.Value).Days * Globals.selectedYacht.napi_ar;
                    break;
                case 2:
                    lbcalkulPrice.Content = dpEndDate.SelectedDate.Value.Subtract(dpStartDate.SelectedDate.Value).Days * Globals.selectedDevice.napi_ar;
                    break;
            }
        }
    }
}
