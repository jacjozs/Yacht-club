using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Data;

namespace Yacht_club
{
    class Database
    {
        private MySqlConnection connect;
        public bool DatabaseConnect = false;
        public void MysqlConnect()
        {
            try
            {
                string connectString = "datasource=87.229.71.159;port=3306;username=gamf;password=FQQ9txqAvTTB!;database=gamfYachtClub";
                connect = new MySqlConnection(connectString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public Felhasznalo MysqlFelhasznalo(string login_name, string passwd)
        {
            Felhasznalo user = new Felhasznalo();
            user.login = MysqlLogin(login_name, passwd);
            try
            {
                if (user.login.id != 0)
                {
                    string query = "SELECT * FROM enYacht_Club_Tag WHERE member_id = ?member_id;";
                    using (MySqlCommand cmd = new MySqlCommand(query, connect))
                    {
                        cmd.Parameters.Add("?member_id", MySqlDbType.Int32).Value = user.login.id;
                        MySqlDataReader read = cmd.ExecuteReader();
                        while (read.Read())
                        {
                            user.nickname = read["nickname"].ToString();
                            user.veztek_nev = read["last_name"].ToString();
                            user.kereszt_nev = read["first_name"].ToString();
                            user.orszag = read["country"].ToString();
                            user.lakcim = read["address"].ToString();
                            user.iranyitoszm = int.Parse(read["zip_code"].ToString());
                            user.szuletesdt = DateTime.Parse(read["birthday"].ToString());
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error in adding mysql row. Error: " + ex.Message);
            }
            finally
            {
                connect.Close();
            }
            
            return user;
        }

        private Login MysqlLogin(string login_name, string password)
        {
            Login user = new Login();
            try
            {
                string query = "SELECT * FROM enLogin WHERE login_name = ?login_name AND password = ?password;";
                connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, connect))
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

        public void MysqlRegisztracioLogin(Felhasznalo regisztration)
        {
            try
            {
                string query = "INSERT INTO enLogin(login_id, login_name, password, email, admin) VALUES (?login_id, ?login_name,?password,?email, ?admin);";
                connect.Open();
                int id = MysqlNextId("enLogin", "login_id");
                using (MySqlCommand cmd = new MySqlCommand(query, connect))
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
                connect.Close();
            }
        }

        private void MysqlRegisztracionUser(Felhasznalo regisztration, int id)
        {
            try
            {
                string query = "INSERT INTO enYacht_Club_Tag(member_id, login_id, nickname, last_name, first_name, birthday, zip_code, address, country) VALUES (?member_id, ?login_id, ?nickname, ?last_name, ?first_name, ?birthday, ?zip_code, ?address, ?country);";
                using (MySqlCommand cmd = new MySqlCommand(query, connect))
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

        private int MysqlNextId(string tabla, string mezo)
        {
            int szam = 1;
            try
            {
                string query = "SELECT MAX("+mezo+") FROM "+tabla+";";
                using (MySqlCommand cmd = new MySqlCommand(query, connect))
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
    }
}
