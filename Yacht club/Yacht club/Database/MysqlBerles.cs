using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Yacht_club.Database
{
    class MysqlBerles
    {
        public Dictionary<string, int> MysqlBerlesPortName()
        {
            Dictionary<string, int> login = new Dictionary<string, int>();
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
                        login.Add(read["name"].ToString(), (int)read["port_id"]);
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
    }
}
