using System.Windows.Media;

namespace Yacht_club.Moduls
{
    class Themes
    {
        public int id { get; set; }
        public string nickname { get; set; }
        public Brush hatter { get; set; }
        public Brush betu_szin { get; set; }
        public Brush menu_betu_szin { get; set; }
        public Brush menu_hatter { get; set; }
        public Brush uc_hatter { get; set; }
        public Brush csuszka { get; set; }
        public bool icon_black { get; set; }

        public Themes(int num)
        {
            nickname = Globals.User.nickname + "!";
            switch (num)
            {
                case 1:
                    id = 1;
                    hatter = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2D3446"));
                    betu_szin = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFACB5B9"));
                    menu_betu_szin = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                    menu_hatter = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF007ACC"));
                    uc_hatter = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF1D212C"));
                    icon_black = false;
                    break;
                case 2:
                    id = 2;
                    hatter = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF69C4EC"));
                    betu_szin = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF173542"));
                    menu_betu_szin = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF173542"));
                    menu_hatter = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF007ACC"));
                    uc_hatter = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0A91CC"));
                    icon_black = false;
                    break;
                case 3:
                    id = 3;
                    hatter = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2F5FDB"));
                    betu_szin = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFACB5B9"));
                    menu_betu_szin = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF173542"));
                    menu_hatter = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF69C4EC"));
                    uc_hatter = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF363E54"));
                    icon_black = false;
                    break;
                    ///Pink
                case 4:
                    id = 4;
                    hatter = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD123E6"));
                    betu_szin = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF000000"));
                    menu_betu_szin = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF000000"));
                    menu_hatter = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF0084"));
                    uc_hatter = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9716A7"));
                    icon_black = true;
                    break;
                    ///Green
                case 5:
                    id = 5;
                    hatter = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4FDC3C"));
                    betu_szin = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF000000"));
                    menu_betu_szin = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF000000"));
                    menu_hatter = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF3D9C14"));
                    uc_hatter = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF3D9C14"));
                    icon_black = true;
                    break;
            }
        }
    }
}
