using System.Windows.Controls;
using System.Windows;

namespace Yacht_club.UsingControls
{
    /// <summary>
    /// Interaction logic for ucUzenet.xaml
    /// </summary>
    public partial class ucUzenet : UserControl
    {
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
            }
            else
            {
                if (Globals.selectedMessage.device_id != 0)
                {
                    lbSzNev.Content = Globals.selectedMessage.device_id;
                    lbSzallitoName.Visibility = Visibility.Visible;
                }
            }
            lbMettol.Content = Globals.selectedMessage.kezdete.ToLongDateString();
            lbMeddig.Content = Globals.selectedMessage.vege.ToLongDateString();
            lbIndul.Content = Globals.selectedMessage.honnan_id;
            lbErkez.Content = Globals.selectedMessage.hova_id;
        }
    }
}
