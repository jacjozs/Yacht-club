using System.Windows.Controls;

namespace Yacht_club.UsingControls
{
    /// <summary>
    /// Interaction logic for ucYacht.xaml
    /// </summary>
    public partial class ucYacht : UserControl
    {
        public ucYacht()
        {
            InitializeComponent();
        }

        public void Loading()
        {
            lbYachtnev.Content = Globals.selectedYacht.nev;
        }
    }
}
