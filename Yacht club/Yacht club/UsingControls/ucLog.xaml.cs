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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Yacht_club.UsingControls
{
    /// <summary>
    /// Interaction logic for ucLog.xaml
    /// </summary>
    public partial class ucLog : UserControl
    {
        private StackPanel panel;

        public ucLog()
        {
            InitializeComponent();
        }

        public void Add()
        {
            if (Globals.log != "")
            {
                Style LogStackLabel = Application.Current.FindResource("LogStackLabel") as Style;

                panel = new StackPanel();
                panel.Orientation = Orientation.Horizontal;
                panel.Height = 15;
                panel.Width = 813;
                panel.Margin = new Thickness(0, 5, 0, 0);

                Label log = new Label();
                log.Content = Globals.log;
                log.Style = LogStackLabel;

                panel.Children.Add(log);

                spList.Children.Insert(0, panel);
            }
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            delete();
        }

        public void delete()
        {
            spList.Children.Clear();
            Globals.log = "";
            Globals.Main.logAdd(false);
        }
    }
}
