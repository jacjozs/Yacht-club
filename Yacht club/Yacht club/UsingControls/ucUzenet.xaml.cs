using System.Windows.Controls;

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
        }
    }
}
