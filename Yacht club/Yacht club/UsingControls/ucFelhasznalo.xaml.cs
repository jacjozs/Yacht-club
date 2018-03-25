using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Yacht_club.UsingControls
{
    /// <summary>
    /// Interaction logic for ucFelhasznalo.xaml
    /// </summary>
    public partial class ucFelhasznalo : UserControl
    {
        public ucFelhasznalo()
        {
            InitializeComponent();
        }
        /// <summary>
        /// profil ép konvertálása és betöltése a imgProfil helyre
        /// </summary>
        public void ProfilImgLoad()
        {
            if (Globals.User.kep != null)
            {
                MemoryStream ms = new MemoryStream();
                Globals.User.kep.Save(ms, ImageFormat.Png);
                ms.Seek(0, SeekOrigin.Begin);

                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = ms;
                bitmapImage.EndInit();

                imgProfil.Source = bitmapImage;
            }
        }
    }
}
