using MySql.Data.MySqlClient;
using System.Windows;

namespace Yacht_club.Database
{
    class MysqlRegistration
    {
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
                    cmd.Parameters.Add("?admin", MySqlDbType.Bit).Value = regisztration.login.admin ? 1 : 0;
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
                    szam += (int)cmd.ExecuteScalar();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error in adding mysql row. Error: " + ex.Message);
            }
            return szam;
        }
    }
}
