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
    /// Interaction logic for ucYacht_delete.xaml
    /// </summary>
    public partial class ucYacht_delete : UserControl
    {
        Database.MysqlYacht data;
        public ucYacht_delete()
        {
            InitializeComponent();
            Loading();
        }

        public void Loading()
        {
            data = new Database.MysqlYacht();
            List<Yacht> Yachts = data.MysqlYachtAll();
            lvYachtAll.ItemsSource = Yachts;
        }

        private void lvYachtsDelete_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            Yacht selectYacht = (Yacht)(sender as ListView).SelectedItem;
            if (selectYacht != null)
            {
                MessageBoxResult delete = MessageBox.Show("Biztos törölni szeretnéd?", "Yacht törlés", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (delete == MessageBoxResult.Yes)
                {
                    try
                    {
                        data.MysqlDeleteYacht(selectYacht.id);
                        Loading();
                        Globals.log = "Törlés Sikeres! <Yach>";
                    }
                    catch (Exception)
                    {
                        Globals.log = "Törlés Sikertelen! <Yach>";
                    }
                }
            }
        }
    }
}
