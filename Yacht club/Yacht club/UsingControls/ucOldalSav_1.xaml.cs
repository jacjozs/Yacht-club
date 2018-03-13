using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for ucOldalSav_1.xaml
    /// </summary>
    public partial class ucOldalSav_1 : UserControl
    {
        public ucOldalSav_1()
        {
            InitializeComponent();
        }

        private void Label_MouseLeftButtonDown_Exit(object sender, MouseButtonEventArgs e)
        {
            Thread.Sleep(1000);
            Environment.Exit(0);
        }
    }
}
