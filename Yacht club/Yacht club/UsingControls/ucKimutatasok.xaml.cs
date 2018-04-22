using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Windows.Input;
using Yacht_club.Moduls;
using System;

namespace Yacht_club.UsingControls
{
    /// <summary>
    /// Interaction logic for ucKimutatasok.xaml
    /// </summary>
    public partial class ucKimutatasok : UserControl
    {
        Label[] lbAdatok;
        private wMessage MessageDialog;
        Database.MysqlMessage data;
        private List<Message> Messages;

        public ucKimutatasok()
        {
            InitializeComponent();
            Loading();



        }
        private void Loading()
        {
            lbAdatok = new Label[]
            {
             lbAprilis,lbAugusztus,lbDecember,lbEv,lbFebruar,lbJanuar,lbJulius,lbJunius,lbMajus,lbMarcius,lbNovember,lbOktober,lbSzeptember
            };

            data = new Database.MysqlMessage();
            Messages = data.MysqlAllMessages();



            int[] db = new int[13];

            for (int j = 0; j < 12; j++)
            {
                for (int i = 0; i < Messages.Count; i++)
                {
                    DateTime ev = Messages[i].keletkezett;
                    DateTime most = DateTime.Now;

                    if ((int)Messages[i].yacht_id != 0 && ev.Year == most.Year && ev.Month == j + 1)
                    {
                        db[j]++;
                        db[12]++;
                    }
                }
            }
            lbEv.Content = db[12] + " db";
            lbJanuar.Content = db[0] + " db";
            lbFebruar.Content = db[1] + " db";
            lbMarcius.Content = db[2] + " db";
            lbAprilis.Content = db[3] + " db";
            lbMajus.Content = db[4] + " db";
            lbJunius.Content = db[5] + " db";
            lbJulius.Content = db[6] + " db";
            lbAugusztus.Content = db[7] + " db";
            lbSzeptember.Content = db[8] + " db";
            lbOktober.Content = db[9] + " db";
            lbNovember.Content = db[10] + " db";
            lbDecember.Content = db[11] + " db";

        }
    }
}
