using System.Collections.Generic;
using System.Windows.Controls;

namespace Yacht_club.UsingControls
{
    /// <summary>
    /// Interaction logic for ucBerles.xaml
    /// </summary>
    public partial class ucBerles : UserControl
    {
        private Database.MysqlBerles data;
        private Dictionary<string, int> list;
        public ucBerles()
        {
            InitializeComponent();
            Loading();
        }
        private void Loading()
        {
            data = new Database.MysqlBerles();
            list = data.MysqlBerlesPortName();
            cbBerlesFrom.ItemsSource = list.Keys;
            cbBerlesTo.ItemsSource = list.Keys;
        }
    }
}
