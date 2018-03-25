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
    /// Interaction logic for ucErtesitesek.xaml
    /// </summary>
    public partial class ucErtesitesek : UserControl
    {
        private Main_Yacht_Window log;
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

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            lbLog.Items.Clear();
            Globals.log = "";
            log = new Main_Yacht_Window();
            log.logAdd(true);
        }
    }
}
