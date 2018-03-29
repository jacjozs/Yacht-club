using MySql.Data.MySqlClient;
using System;
using System.Windows;
using System.Drawing;
using System.IO;

namespace Yacht_club.Database
{
    class MysqlLogin
    {
        /// <summary>
        /// Bejelentkezés és "ellenörzés"
        /// </summary>
        /// <param name="login_name">A login név</param>
        /// <param name="password">A jelszó</param>
        /// <returns></returns>
        private Login Mysql_Login(string login_name, string password)
        {
            Login user = null;
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
                        user = new Login();
                        user.email = read["email"].ToString();
                        user.utolsoLogin = DateTime.Parse(read["last_login"].ToString());
                        user.felhasznalonev = login_name;
                        user.jelszo = password;
                        user.theme = (int)read["theme"];
                        if ((int)read["admin"] == 1)
                            user.admin = true;
                        else user.admin = false;
                        user.id = (int)read["login_id"];
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
                if (user.login != null)
                {
                    string query = "SELECT * FROM enYacht_Club_Tag INNER JOIN enzipcode USING(zip_code) WHERE member_id = ?member_id;";
                    using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                    {
                        cmd.Parameters.Add("?member_id", MySqlDbType.Int32).Value = user.login.id;
                        MySqlDataReader read = cmd.ExecuteReader();
                        while (read.Read())
                        {
                            user.member_id = (int)read["member_id"];
                            user.nickname = read["nickname"].ToString();
                            user.veztek_nev = read["last_name"].ToString();
                            user.kereszt_nev = read["first_name"].ToString();
                            user.orszag = read["country"].ToString();
                            user.lakcim = read["address"].ToString();
                            user.iranyitoszm = (int)read["zip_code"];
                            user.varos = read["city"].ToString();
                            user.szuletesdt = DateTime.Parse(read["birthday"].ToString());
                            try
                            { user.kep = ByteToImage((byte[])read["image"]); }
                            catch (Exception)
                            { user.kep = null; }
                        }
                    }
                }
                else return null;
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
    }
}
