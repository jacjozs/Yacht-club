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
        public ucFelhasznalo()
        {
            InitializeComponent();
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
            if (Globals.User.nickname != null) lbFNev.Content = Globals.User.nickname; else lbFNev.Content = "Nincs kitöltve";
            if (Globals.User.veztek_nev != null) lbVNev.Content = Globals.User.veztek_nev; else lbVNev.Content = "Nincs kitöltve";
            if (Globals.User.kereszt_nev != null) lbKNev.Content = Globals.User.kereszt_nev; else lbKNev.Content = "Nincs kitöltve";
            if (Globals.User.szuletesdt != null) lbSzEv.Content = Globals.User.szuletesdt.ToLongDateString(); else lbSzEv.Content = "Nincs kitöltve";
            if (Globals.User.orszag != null) lbOrszag.Content = Globals.User.orszag; else lbOrszag.Content = "Nincs kitöltve";
            if (Globals.User.iranyitoszm != 0) lbIrszam.Content = Globals.User.iranyitoszm; else lbIrszam.Content = "Nincs kitöltve";
            if (Globals.User.varos != null) lbVaros.Content = Globals.User.varos; else lbVaros.Content = "Nincs kitöltve";
            if (Globals.User.lakcim != null) lbLakcim.Content = Globals.User.lakcim; else lbLakcim.Content = "Nincs kitöltve";
        }
        /// <summary>
        /// profil kép konvertálása és betöltése a imgProfil helyre
        /// </summary>
        public void ProfilImgLoad()
        {
            if (Globals.User.kep != null)
            {
                imgProfil.Source = Globals.User.kep;
            }
        }
    }
}
