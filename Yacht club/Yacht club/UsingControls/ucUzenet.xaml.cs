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
    /// Interaction logic for ucUzenet.xaml
    /// </summary>
    public partial class ucUzenet : UserControl
    {
        public ucUzenet()
        {
            InitializeComponent();
            labelid.Content = Globals.selectedMessage.uzenet_id;
        }
    }
}
