using System;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Yacht_club
{
    class Database
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
            user.login = MysqlLogin(login_name, password);
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
        /// Bejelentkezés és "ellenörzés"
        /// </summary>
        /// <param name="login_name">A login név</param>
        /// <param name="password">A jelszó</param>
        /// <returns></returns>
        private Login MysqlLogin(string login_name, string password)
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

        /// <summary>
        /// Új Felhasználói login beírása a Login táblába
        /// </summary>
        /// <param name="regisztration">Az új felhasználó</param>
        public void MysqlRegisztracioLogin(Felhasznalo regisztration)
        {
            try
            {
                string query = "INSERT INTO enLogin(login_id, login_name, password, email, admin) VALUES (?login_id, ?login_name,?password,?email, ?admin);";
                Globals.connect.Open();
                int id = MysqlNextId("enLogin", "login_id");
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.Parameters.Add("?login_id", MySqlDbType.Int16).Value = id;
                    cmd.Parameters.Add("?login_name", MySqlDbType.VarChar).Value = regisztration.login.felhasznalonev;
                    cmd.Parameters.Add("?password", MySqlDbType.VarChar).Value = regisztration.login.jelszo;
                    cmd.Parameters.Add("?email", MySqlDbType.VarChar).Value = regisztration.login.email;
                    cmd.Parameters.Add("?admin", MySqlDbType.Bit).Value = regisztration.login.admin? 1 : 0;
                    cmd.ExecuteNonQuery();
                }
                MysqlRegisztracionUser(regisztration, id);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error in adding mysql row. Error: " + ex.Message);
            }
            finally
            {
                Globals.connect.Close();
            }
        }
        /// <summary>
        /// Új felhasználó beszurása a YachClubTag táblába
        /// </summary>
        /// <param name="regisztration">A felhasználó adatai</param>
        /// <param name="id">A hozzá tartozó login id</param>
        private void MysqlRegisztracionUser(Felhasznalo regisztration, int id)
        {
            try
            {
                string query = "INSERT INTO enYacht_Club_Tag(member_id, login_id, nickname, last_name, first_name, birthday, zip_code, address, country) VALUES (?member_id, ?login_id, ?nickname, ?last_name, ?first_name, ?birthday, ?zip_code, ?address, ?country);";
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.Parameters.Add("?member_id", MySqlDbType.Int16).Value = MysqlNextId("enYacht_Club_Tag", "member_id");
                    cmd.Parameters.Add("?login_id", MySqlDbType.Int16).Value = id;
                    cmd.Parameters.Add("?nickname", MySqlDbType.VarChar).Value = regisztration.nickname;
                    cmd.Parameters.Add("?last_name", MySqlDbType.VarChar).Value = regisztration.kereszt_nev;
                    cmd.Parameters.Add("?first_name", MySqlDbType.VarChar).Value = regisztration.veztek_nev;
                    cmd.Parameters.Add("?birthday", MySqlDbType.DateTime).Value = regisztration.szuletesdt;
                    cmd.Parameters.Add("?zip_code", MySqlDbType.Int16).Value = regisztration.iranyitoszm;
                    cmd.Parameters.Add("?address", MySqlDbType.VarChar).Value = regisztration.lakcim;
                    cmd.Parameters.Add("?country", MySqlDbType.VarChar).Value = regisztration.orszag;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error in adding mysql row. Error: " + ex.Message);
            }
        }
        /// <summary>
        /// Felhasználói rekord frissitése
        /// </summary>
        /// <param name="UpdateUser">A frissteni kivánt felhasználó</param>
        public void MysqlUpdateUser(Felhasznalo UpdateUser)
        {
            try
            {
                string query = "UPDATE enYacht_Club_Tag SET nickname = ?nickname, last_name = ?last_name, first_name = ?first_name, birthday = ?birthday, zip_code = ?zip_code, address = ?address, country = ?country, image = ?image WHERE member_id = ?member_id;";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.Parameters.Add("?nickname", MySqlDbType.VarChar).Value = UpdateUser.nickname;
                    cmd.Parameters.Add("?last_name", MySqlDbType.VarChar).Value = UpdateUser.kereszt_nev;
                    cmd.Parameters.Add("?first_name", MySqlDbType.VarChar).Value = UpdateUser.veztek_nev;
                    cmd.Parameters.Add("?birthday", MySqlDbType.DateTime).Value = UpdateUser.szuletesdt;
                    cmd.Parameters.Add("?zip_code", MySqlDbType.Int16).Value = UpdateUser.iranyitoszm;
                    cmd.Parameters.Add("?address", MySqlDbType.VarChar).Value = UpdateUser.lakcim;
                    cmd.Parameters.Add("?country", MySqlDbType.VarChar).Value = UpdateUser.orszag;
                    cmd.Parameters.Add("?image", MySqlDbType.Blob).Value = ImageToByte(UpdateUser.kep);
                    cmd.Parameters.Add("?member_id", MySqlDbType.Int16).Value = UpdateUser.member_id;
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception)
            {
                Globals.log = "Sikertelen módosítás!";
            }
            finally
            {
                Globals.connect.Close();
            }
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
                string query = "SELECT MAX("+mezo+") FROM "+tabla+";";
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
