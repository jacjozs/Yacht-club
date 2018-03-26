using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.IO;

namespace Yacht_club.Database
{
    class MysqlLogin
    {
        /// <summary>
        /// Bejelentkezés és az felhasználó adatainak a beolvasása
        /// </summary>
        /// <param name="login_name">Login név</param>
        /// <param name="password">Jelszó</param>
        /// <returns></returns>
        public Felhasznalo MysqlFelhasznalo(string login_name, string password)
        {
            Felhasznalo user = new Felhasznalo();
            user.login = Mysql_Login(login_name, password);
            try
            {
                if (user.login.id != 0)
                {
                    string query = "SELECT * FROM enYacht_Club_Tag WHERE member_id = ?member_id;";
                    using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                    {
                        cmd.Parameters.Add("?member_id", MySqlDbType.Int32).Value = user.login.id;
                        MySqlDataReader read = cmd.ExecuteReader();
                        while (read.Read())
                        {
                            user.member_id = int.Parse(read["member_id"].ToString());
                            user.nickname = read["nickname"].ToString();
                            user.veztek_nev = read["last_name"].ToString();
                            user.kereszt_nev = read["first_name"].ToString();
                            user.orszag = read["country"].ToString();
                            user.lakcim = read["address"].ToString();
                            user.iranyitoszm = int.Parse(read["zip_code"].ToString());
                            user.szuletesdt = DateTime.Parse(read["birthday"].ToString());
                            try
                            { user.kep = ByteToImage((byte[])read["image"]); }
                            catch (Exception)
                            { user.kep = null; }
                        }
                    }
                    user.varos = MysqlZipToCity(user.iranyitoszm);
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
            return user;
        }

        /// <summary>
        /// Az átvett byte tömböt képé alakitja
        /// </summary>
        /// <param name="imageByte"></param>
        /// <returns></returns>
        private Image ByteToImage(byte[] imageByte)
        {
            Image image = null;
            using (MemoryStream ms = new MemoryStream(imageByte))
            {
                image = Image.FromStream(ms);
            }
            return image;
        }

        /// <summary>
        /// Bejelentkezés és "ellenörzés"
        /// </summary>
        /// <param name="login_name">A login név</param>
        /// <param name="password">A jelszó</param>
        /// <returns></returns>
        private Login Mysql_Login(string login_name, string password)
        {
            Login user = new Login();
            try
            {
                string query = "SELECT * FROM enLogin WHERE login_name = ?login_name AND password = ?password;";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.Parameters.Add("?login_name", MySqlDbType.VarChar).Value = login_name;
                    cmd.Parameters.Add("?password", MySqlDbType.VarChar).Value = password;
                    MySqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        user.email = read["email"].ToString();
                        user.utolsoLogin = DateTime.Parse(read["last_login"].ToString());
                        user.felhasznalonev = login_name;
                        user.jelszo = password;
                        if (int.Parse(read["admin"].ToString()) == 1)
                            user.admin = true;
                        else user.admin = false;
                        user.id = int.Parse(read["login_id"].ToString());
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error in adding mysql row. Error: " + ex.Message);
            }
            return user;
        }

        /// <summary>
        /// Város kikeresése írányitószám alapján
        /// </summary>
        /// <param name="zipcode">írányitószám</param>
        /// <returns>Város</returns>
        private string MysqlZipToCity(int zipcode)
        {
            string city = "";
            try
            {
                string query = "SELECT city FROM enZipcode WHERE zip_code = ?zip_code;";
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.Parameters.Add("?zip_code", MySqlDbType.Int32).Value = zipcode;
                    city = cmd.ExecuteScalar().ToString();
                }
                return city;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error in adding mysql row. Error: " + ex.Message);
            }
            return city;
        }
    }
}
