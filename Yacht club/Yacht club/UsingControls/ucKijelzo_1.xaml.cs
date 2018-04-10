using System.Windows.Controls;
using Yacht_club.UsingControls.Minis;

namespace Yacht_club.UsingControls
{
    /// <summary>
    /// Interaction logic for ucKijelzo_1.xaml
    /// </summary>
    public partial class ucKijelzo_1 : UserControl
    {
        public ucKijelzo_1()
        {
            InitializeComponent();
            Globals.UpdateHistory();
            ccWindow_Uzenet.Content = new ucUzenetek_2();
            ccWindow_Kimutat.Content = new ucKimutatasok_2();
            ccWindow_Szallito.Content = new ucSzallitok_2();
            ccWindow_Yacht.Content = new ucYachtok_2();
        }
    }
}
