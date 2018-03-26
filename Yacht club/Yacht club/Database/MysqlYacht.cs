using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace Yacht_club.Database
{
    class MysqlYacht
    {
        public bool MysqlRegisztracionYacht(Yacht yacht)
        {
            try
            {
                string query = "INSERT INTO enYacht(yacht_id, name, type, image, ower, seats, width, lenght, height, weight) VALUES (?yacht_id, ?name, ?type, ?image, ?ower, ?seats, ?width, ?lenght, ?height, ?weight);";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.Parameters.Add("?yacht_id", MySqlDbType.Int16).Value = MysqlNextId("enYacht", "yacht_id");
                    cmd.Parameters.Add("?name", MySqlDbType.VarChar).Value = yacht.nev;
                    cmd.Parameters.Add("?type", MySqlDbType.VarChar).Value = yacht.tipus;
                    cmd.Parameters.Add("?image", MySqlDbType.Blob).Value = ImageToByte(yacht.kep);
                    cmd.Parameters.Add("?ower", MySqlDbType.Int16).Value = yacht.login_id;
                    cmd.Parameters.Add("?seats", MySqlDbType.Int16).Value = yacht.ferohely;
                    cmd.Parameters.Add("?width", MySqlDbType.Int16).Value = yacht.szeles;
                    cmd.Parameters.Add("?lenght", MySqlDbType.Int16).Value = yacht.hossz;
                    cmd.Parameters.Add("?height", MySqlDbType.Int16).Value = yacht.magas;
                    cmd.Parameters.Add("?weight", MySqlDbType.Int16).Value = yacht.suly;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error in adding mysql row. Error: " + ex.Message);
                return false;
            }
            finally
            {
                Globals.connect.Close();
            }
            return true;
        }

        public Dictionary<string, int> MysqlRegisztracionYachtLoginName()
        {
            Dictionary<string, int> login = new Dictionary<string, int>();
            try
            {
                string query = "SELECT login_id, login_name FROM enlogin;";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    MySqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        login.Add(read["login_name"].ToString(), int.Parse(read["login_id"].ToString()));
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
        /// Megkeresi a legnagyobb értéket a megatod mezöben és táblában és megnöveli egyel
        /// </summary>
        /// <param name="tabla">A tábla neve</param>
        /// <param name="mezo">A mező neve</param>
        /// <returns>A megnövelt szám</returns>
        private int MysqlNextId(string tabla, string mezo)
        {
            int szam = 1;
            try
            {
                string query = "SELECT MAX(" + mezo + ") FROM " + tabla + ";";
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.Parameters.Add("?tabla", MySqlDbType.VarChar).Value = tabla;
                    szam += int.Parse(cmd.ExecuteScalar().ToString());
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error in adding mysql row. Error: " + ex.Message);
            }
            return szam;
        }
        /// <summary>
        /// Az átvett Image-t byte tömbre alakitja
        /// </summary>
        /// <param name="Image"></param>
        /// <returns></returns>
        private byte[] ImageToByte(Image Image)
        {
            byte[] imageData = null;
            using (MemoryStream ms = new MemoryStream())
            {
                Image.Save(ms, Image.RawFormat);
                imageData = ms.ToArray();
            }
            return imageData;
        }
    }
}
