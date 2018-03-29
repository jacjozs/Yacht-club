using System.Windows;
using System.Windows.Controls;

namespace Yacht_club.UsingControls
{
    /// <summary>
    /// Interaction logic for ucErtesitesek.xaml
    /// </summary>
    public partial class ucErtesitesek : UserControl
    {
        public ucErtesitesek()
        {
            InitializeComponent();
        }

        public void Add()
        {
            if (Globals.log != "")
            {
                lbLog.Items.Insert(0, Globals.log);
            }
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            delete();
        }

        public void delete()
        {
            lbLog.Items.Clear();
            Globals.log = "";
            Globals.Main.logAdd(true);
        }
    }
}
