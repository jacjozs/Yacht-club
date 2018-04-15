using System.Drawing.Imaging;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Yacht_club.UsingControls
{
    /// <summary>
    /// Interaction logic for ucFelhasznalo.xaml
    /// </summary>
    public partial class ucFelhasznalo : UserControl
    {
        private Label[] lbAdatok;
        private Database.MysqlMessage data;
        private Felhasznalo User;
        public ucFelhasznalo(int id)
        {
            InitializeComponent();
            if (id != 0)
            {
                data = new Database.MysqlMessage();
                User = data.MysqlFelhasznaloData(id);
                data = null;
            }
            else User = Globals.User;
            Loading();
            Globals.UpdateHistory();
        }


        private void Loading()
        {
            ProfilImgLoad();
            lbAdatok = new Label[]
            {
             lbFNev,lbVNev,lbKNev,lbSzEv,lbOrszag,lbVaros,lbIrszam,lbLakcim
            };

            // label feltöltése adatokkal
            if (User.nickname != null) lbFNev.Content = User.nickname; else lbFNev.Content = "Nincs kitöltve";
            if (User.veztek_nev != null) lbVNev.Content = User.veztek_nev; else lbVNev.Content = "Nincs kitöltve";
            if (User.kereszt_nev != null) lbKNev.Content = User.kereszt_nev; else lbKNev.Content = "Nincs kitöltve";
            if (User.szuletesdt != null) lbSzEv.Content = User.szuletesdt.ToLongDateString(); else lbSzEv.Content = "Nincs kitöltve";
            if (User.orszag != null) lbOrszag.Content = User.orszag; else lbOrszag.Content = "Nincs kitöltve";
            if (User.iranyitoszm != 0) lbIrszam.Content = User.iranyitoszm; else lbIrszam.Content = "Nincs kitöltve";
            if (User.varos != null) lbVaros.Content = User.varos; else lbVaros.Content = "Nincs kitöltve";
            if (User.lakcim != null) lbLakcim.Content = User.lakcim; else lbLakcim.Content = "Nincs kitöltve";
        }
        /// <summary>
        /// profil kép konvertálása és betöltése a imgProfil helyre
        /// </summary>
        public void ProfilImgLoad()
        {
            if (User.kep != null)
            {
                imgProfil.Source = User.kep;
            }
        }
    }
}
