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
    /// Interaction logic for ucYachtok.xaml
    /// </summary>
    public partial class ucYachtok : UserControl
    {
        Database.MysqlYacht data;
        public ucYachtok()
        {
            InitializeComponent();
            Loading();
        }

        public void Loading()
        {
            data = new Database.MysqlYacht();
            List<Yacht> Yachts = data.MysqlYachts(Globals.User.member_id);
            lvYachts.ItemsSource = Yachts;
        }

        private void lvYachts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            Yacht selectYacht = (Yacht)(sender as ListView).SelectedItem;
            if (selectYacht != null)
            {
                Globals.selectedYacht = selectYacht;
                ucYacht yacht = new ucYacht();
                yacht.Loading();
                Globals.Main.ccWindow_2.Content = yacht;
            }
        }
    }
}
