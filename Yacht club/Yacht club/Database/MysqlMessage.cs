using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Yacht_club.Moduls;

namespace Yacht_club.Database
{
    class MysqlMessage
    {
        public List<Message> MysqlUserMessages(int id)
        {
            List<Message> uzenetek = new List<Message>();
            try
            {
                string query = "SELECT enMessage.*, enYacht_Club_Tag.* FROM enMessage INNER JOIN enYacht_Club_Tag ON enYacht_Club_Tag.member_id = enMessage.sender WHERE addressee = ?addressee;";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.Parameters.Add("?addressee", MySqlDbType.Int16).Value = id; 
                    MySqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        Message uzenet = new Message();
                        uzenet.uzenet_id = (int)read["message_id"];
                        uzenet.cimzett_id = id;
                        uzenet.cimzett_nev = Globals.User.teljes_nev;
                        uzenet.felado_id = (int)read["sender"];
                        uzenet.felado_nev = read["first_name"].ToString() + " " + read["last_name"].ToString();
                        uzenet.honnan_id = (int)read["from_port"];
                        uzenet.hova_id = (int)read["to_port"];
                        uzenet.kezdete = (DateTime)read["start_date"];
                        uzenet.vege = (DateTime)read["end_date"];
                        if (!Convert.IsDBNull(read["yacht_id"]))
                        {
                            uzenet.yacht_id = (int)read["yacht_id"];
                        }
                        if (!Convert.IsDBNull(read["device_id"]))
                            uzenet.device_id = (int)read["device_id"];
                        uzenet.keletkezett = (DateTime)read["date"];
                        if (read["accept"].ToString() != "")
                        {
                            switch ((int)read["accept"])
                            {
                                case 0:
                                    uzenet.elfogadvabl = false;
                                    uzenet.elfogadvastr = "Elutasítva";
                                    break;
                                case 1:
                                    uzenet.elfogadvabl = true;
                                    uzenet.elfogadvastr = "Elfogadva";
                                    break;
                            }
                        }
                        if ((int)read["new"] == 1)
                        {
                            uzenet.NEWbl = true;
                            uzenet.NEWstr = "Új üzenet!";
                        } else
                        {
                            uzenet.NEWbl = false;
                        }
                        uzenetek.Add(uzenet);
                    }
                }
                YachtName(uzenetek);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error in adding mysql row. Error: " + ex.Message);
            }
            finally
            {
                Globals.connect.Close();
            }
            return uzenetek;
        }

        public List<Message> MysqlUserNewMessages(int id)
        {
            List<Message> uzenetek = new List<Message>();
            try
            {
                string query = "SELECT enMessage.*, enYacht_Club_Tag.* FROM enMessage INNER JOIN enYacht_Club_Tag ON enYacht_Club_Tag.member_id = enMessage.sender WHERE addressee = ?addressee and new = 1;";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.Parameters.Add("?addressee", MySqlDbType.Int16).Value = id;
                    MySqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        Message uzenet = new Message();
                        uzenet.uzenet_id = (int)read["message_id"];
                        uzenet.cimzett_id = id;
                        uzenet.cimzett_nev = Globals.User.teljes_nev;
                        uzenet.felado_id = (int)read["sender"];
                        uzenet.felado_nev = read["first_name"].ToString() + " " + read["last_name"].ToString();
                        uzenet.honnan_id = (int)read["from_port"];
                        uzenet.hova_id = (int)read["to_port"];
                        uzenet.kezdete = (DateTime)read["start_date"];
                        uzenet.vege = (DateTime)read["end_date"];
                        if (!Convert.IsDBNull(read["yacht_id"]))
                        {
                            uzenet.yacht_id = (int)read["yacht_id"];
                        }
                        if (!Convert.IsDBNull(read["device_id"]))
                            uzenet.device_id = (int)read["device_id"];
                        uzenet.keletkezett = (DateTime)read["date"];
                        if (read["accept"].ToString() != "")
                        {
                            switch ((int)read["accept"])
                            {
                                case 0:
                                    uzenet.elfogadvabl = false;
                                    uzenet.elfogadvastr = "Elutasítva";
                                    break;
                                case 1:
                                    uzenet.elfogadvabl = true;
                                    uzenet.elfogadvastr = "Elfogadva";
                                    break;
                            }
                        }
                        if ((int)read["new"] == 1)
                        {
                            uzenet.NEWbl = true;
                            uzenet.NEWstr = "Új üzenet!";
                        }
                        else
                        {
                            uzenet.NEWbl = false;
                        }
                        uzenetek.Add(uzenet);
                    }
                }
                YachtName(uzenetek);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error in adding mysql row. Error: " + ex.Message);
            }
            finally
            {
                Globals.connect.Close();
            }
            return uzenetek;
        }

        private void YachtName(List<Message> list)
        {
            try
            {
                string query = "SELECT name FROM enYacht WHERE yacht_id = ?yacht_id;";
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].yacht_id == 0) continue;
                    using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                    {
                        cmd.Parameters.Add("?yacht_id", MySqlDbType.Int16).Value = list[i].yacht_id;
                        list[i].yacht_nev = cmd.ExecuteScalar().ToString();
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error in adding mysql row. Error: " + ex.Message);
            }
        }

        public void MysqlMessageDelete(int id)
        {
            try
            {
                string query = "DELETE FROM enMessage WHERE message_id = ?message_id;";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.Parameters.Add("?message_id", MySqlDbType.Int16).Value = id;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error in deleting mysql row. Error: " + ex.Message);
            }
            finally
            {
                Globals.connect.Close();
            }
        }
    }
}
