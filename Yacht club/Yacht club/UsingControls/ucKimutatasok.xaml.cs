using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Yacht_club.UsingControls
{
    /// <summary>
    /// Interaction logic for ucKimutatasok.xaml
    /// </summary>
    public partial class ucKimutatasok : UserControl
    {
        private Point[] points;
        public ucKimutatasok()
        {
            InitializeComponent();
            Globals.UpdateHistory();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);

            GridCursor.Margin = new Thickness(10 + (162.6 * index), 45, 0, 329);
        }
    }
}
