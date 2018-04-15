using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows;
using Yacht_club.Moduls;

namespace Yacht_club.Database
{
    class MysqlBerles
    {
        public Dictionary<int, string> MysqlBerlesPortName()
        {
            Dictionary<int, string> login = new Dictionary<int, string>();
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
                        login.Add((int)read["port_id"], read["name"].ToString());
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

        public void MysqlNewBerles(Message uzenet)
        {
            try
            {
                string query = "INSERT INTO enMessage(message_id, sender, addressee, yacht_id, device_id, start_date, end_date, from_port, to_port) VALUES (?message_id, ?sender, ?addressee, ?yacht_id, ?device_id, ?start_date, ?end_date, ?from_port, ?to_port);";
                Globals.connect.Open();
                int id = MysqlNextId("enMessage", "message_id");
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.Parameters.Add("?message_id", MySqlDbType.Int16).Value = id;
                    cmd.Parameters.Add("?sender", MySqlDbType.Int16).Value = Globals.User.member_id;
                    cmd.Parameters.Add("?addressee", MySqlDbType.Int16).Value = uzenet.cimzett_id;
                    if (uzenet.yacht_id != -1)
                    {
                        cmd.Parameters.Add("?yacht_id", MySqlDbType.Int16).Value = uzenet.yacht_id;
                        cmd.Parameters.Add("?device_id", MySqlDbType.Int16).Value = DBNull.Value;
                    }
                    else if(uzenet.device_id != -1)
                    {
                        cmd.Parameters.Add("?yacht_id", MySqlDbType.Int16).Value = DBNull.Value;
                        cmd.Parameters.Add("?device_id", MySqlDbType.Int16).Value = uzenet.device_id;
                    }
                    if (uzenet.honnan_id != -1 && uzenet.hova_id != -1)
                    {
                        cmd.Parameters.Add("?from_port", MySqlDbType.Int16).Value = uzenet.honnan_id;
                        cmd.Parameters.Add("?to_port", MySqlDbType.Int16).Value = uzenet.hova_id;
                    }
                    else
                    {
                        cmd.Parameters.Add("?from_port", MySqlDbType.Int16).Value = DBNull.Value;
                        cmd.Parameters.Add("?to_port", MySqlDbType.Int16).Value = DBNull.Value;
                    }
                    cmd.Parameters.Add("?start_date", MySqlDbType.DateTime).Value = uzenet.kezdete;
                    cmd.Parameters.Add("?end_date", MySqlDbType.DateTime).Value = uzenet.vege;
                    cmd.ExecuteNonQuery();
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
