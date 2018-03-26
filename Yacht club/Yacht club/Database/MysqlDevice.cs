using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Yacht_club.Database
{
    class MysqlDevice
    {
        public int MysqlAddDevice(Device device)
        {
            int id = 0;
            try
            {
                string query = "INSERT INTO enDevice(device_id, type, ower, max_width, max_lenght, max_height, max_weight) VALUES (?device_id, ?type, ?ower, ?max_width, ?max_lenght, ?max_height, ?max_weight);";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    id = MysqlNextId("enDevice", "device_id");
                    cmd.Parameters.Add("?device_id", MySqlDbType.Int16).Value = id;
                    cmd.Parameters.Add("?type", MySqlDbType.VarChar).Value = device.tipus;
                    cmd.Parameters.Add("?ower", MySqlDbType.Int16).Value = device.login_id;
                    cmd.Parameters.Add("?max_width", MySqlDbType.Int16).Value = device.max_szeles;
                    cmd.Parameters.Add("?max_lenght", MySqlDbType.Int16).Value = device.max_hossz;
                    cmd.Parameters.Add("?max_height", MySqlDbType.Int16).Value = device.max_magas;
                    cmd.Parameters.Add("?max_weight", MySqlDbType.Int16).Value = device.max_suly;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException)
            {
                return 0;
            }
            finally
            {
                Globals.connect.Close();
            }
            return id;
        }

        public Dictionary<string, int> MysqlDeviceLoginName()
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
    }
}
