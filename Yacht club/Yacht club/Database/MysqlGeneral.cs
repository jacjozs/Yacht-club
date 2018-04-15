using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Yacht_club.Database
{
    public static class MysqlGeneral
    {
        public static Dictionary<int, string> MysqlZipCodeName()
        {
            Dictionary<int, string> ZipCode = new Dictionary<int, string>();
            try
            {
                string query = "SELECT zip_code, city FROM enZipcode;";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.ExecuteNonQuery();
                    MySqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        ZipCode.Add((int)read["zip_code"], read["city"].ToString());
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error in adding mysql row. Error: " + ex.Message);
            }
            finally
            {
                Globals.connect.Close();
            }
            return ZipCode;
        }
        /// <summary>
        /// kikötök "map"-ba kigyüjti hogy a name lesz a kulcs
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> MysqlPortName()
        {
            Dictionary<int, string> ports = new Dictionary<int, string>();
            try
            {
                string query = "SELECT port_id, name FROM enPort;";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.ExecuteNonQuery();
                    MySqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        ports.Add((int)read["port_id"], read["name"].ToString());
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error in adding mysql row. Error: " + ex.Message);
            }
            finally
            {
                Globals.connect.Close();
            }
            return ports;
        }

        public static List<string> MysqlLoginNames()
        {
            List<string> LoginName = new List<string>();
            try
            {
                string query = "SELECT login_name FROM enLogin;";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.ExecuteNonQuery();
                    MySqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        LoginName.Add(read["login_name"].ToString());
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error in adding mysql row. Error: " + ex.Message);
            }
            finally
            {
                Globals.connect.Close();
            }
            return LoginName;
        }

        public static List<string> MysqlEmails()
        {
            List<string> Email = new List<string>();
            try
            {
                string query = "SELECT email FROM enLogin;";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.ExecuteNonQuery();
                    MySqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        Email.Add(read["email"].ToString());
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error in adding mysql row. Error: " + ex.Message);
            }
            finally
            {
                Globals.connect.Close();
            }
            return Email;
        }

        /// <summary>
        /// Megkeresi a legnagyobb értéket a megatod mezöben és táblában és megnöveli egyel
        /// </summary>
        /// <param name="tabla">A tábla neve</param>
        /// <param name="mezo">A mező neve</param>
        /// <returns>A megnövelt szám</returns>
        public static int MysqlNextId(string tabla, string mezo)
        {
            int szam = 1;
            try
            {
                string query = "SELECT MAX(" + mezo + ") FROM " + tabla + ";";
                MySqlCommand cmd = new MySqlCommand(query, Globals.connect);
                szam += (int)cmd.ExecuteScalar();
            }
            catch (InvalidCastException)
            {
                return 1;
            }
            return szam;
        }
        /// <summary>
        /// teljesnév és id kigyüjtése és egy "map"-ban való eltárolása 
        /// teljesnév a kulcs!
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> MysqlLoginName()
        {
            string full_name;
            Dictionary<int, string> login = new Dictionary<int, string>();
            try
            {
                string query = "SELECT first_name, last_name, member_id FROM enYacht_Club_Tag;";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.ExecuteNonQuery();
                    MySqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        full_name = "";
                        full_name += read["first_name"].ToString();
                        full_name += " " + read["last_name"].ToString();
                        login.Add((int)read["member_id"], full_name);
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error in adding mysql row. Error: " + ex.Message);
            }
            finally
            {
                Globals.connect.Close();
            }
            return login;
        }

        /// <summary>
        /// Az átvett byte tömböt képé alakitja
        /// </summary>
        /// <param name="imageByte"></param>
        /// <returns></returns>
        public static BitmapImage ByteToImage(byte[] imageByte)
        {
            MemoryStream stream = new MemoryStream(imageByte);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = stream;
            image.EndInit();
            return image;
        }


        /// <summary>
        /// Az átvett Image-t byte tömbre alakitja
        /// </summary>
        /// <param name="Image"></param>
        /// <returns></returns>
        public static byte[] ImageToByte(BitmapImage Image)
        {
            var ms = new MemoryStream();
            var pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(BitmapFrame.Create(Image));
            pngEncoder.Save(ms);

            return ms.GetBuffer();
        }
    }
}
