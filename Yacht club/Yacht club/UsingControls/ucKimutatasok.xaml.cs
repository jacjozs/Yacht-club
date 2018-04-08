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
            points = new Point[5];
            points[0].X = 0;
            points[0].Y = 164;

            points[1].X = 75;
            points[1].Y = 200;

            points[2].X = 150;
            points[2].Y = 150;

            points[3].X = 225;
            points[3].Y = 220;

            points[4].X = 300;
            points[4].Y = 100;

            Polyline line = new Polyline();
            PointCollection collection = new PointCollection();
            foreach (Point pont in points)
            {
                collection.Add(pont);
            }
            line.Stretch = Stretch.Fill;
            line.Points = collection;
            line.VerticalAlignment = VerticalAlignment.Center;
            line.HorizontalAlignment = HorizontalAlignment.Left;
            line.Stroke = Globals.MainTheme.uc_hatter;
            line.StrokeThickness = 15;
            GridDiagrams.Children.Add(line);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);

            GridCursor.Margin = new Thickness(10 + (162.6 * index), 45, 0, 329);
        }
    }
}
