using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Yacht_club.Moduls;

namespace Yacht_club.Database
{
    class MysqlMessage
    {

        public void MysqlNewMessage(Message uzenet)
        {
            try
            {
                string query = "INSERT INTO enMessage(message_id, price, sender, addressee, yacht_id, device_id, start_date, end_date, from_port, to_port) VALUES (?message_id, ?price, ?sender, ?addressee, ?yacht_id, ?device_id, ?start_date, ?end_date, ?from_port, ?to_port);";
                Globals.connect.Open();
                int id = MysqlGeneral.MysqlNextId("enMessage", "message_id");
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.Parameters.Add("?message_id", MySqlDbType.Int16).Value = id;
                    cmd.Parameters.Add("?price", MySqlDbType.Int16).Value = uzenet.price;
                    cmd.Parameters.Add("?sender", MySqlDbType.Int16).Value = Globals.User.member_id;
                    cmd.Parameters.Add("?addressee", MySqlDbType.Int16).Value = uzenet.cimzett_id;
                    if (uzenet.yacht_id != -1)
                    {
                        cmd.Parameters.Add("?yacht_id", MySqlDbType.Int16).Value = uzenet.yacht_id;
                        cmd.Parameters.Add("?device_id", MySqlDbType.Int16).Value = DBNull.Value;
                    }
                    else if (uzenet.device_id != -1)
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
        /// Összes üzenet
        /// </summary>
        /// <returns></returns>
        public List<Message> MysqlAllMessages()
        {
            List<Message> uzenetek = new List<Message>();
            try
            {
                string query = "SELECT enMessage.*, enYacht_Club_Tag.* FROM enMessage INNER JOIN enYacht_Club_Tag ON enYacht_Club_Tag.member_id = enMessage.sender;";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.Parameters.Add("?addressee", MySqlDbType.Int16).Value = Globals.User.member_id;
                    cmd.Parameters.Add("?sender", MySqlDbType.Int16).Value = Globals.User.member_id;
                    MySqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        Message uzenet = new Message();
                        uzenet.uzenet_id = (int)read["message_id"];
                        uzenet.cimzett_id = (int)read["addressee"];
                        uzenet.felado_id = (int)read["sender"];
                        if (!Convert.IsDBNull(read["from_port"]))
                            uzenet.honnan_id = (int)read["from_port"];
                        if (!Convert.IsDBNull(read["to_port"]))
                            uzenet.hova_id = (int)read["to_port"];
                        uzenet.kezdete = (DateTime)read["start_date"];
                        uzenet.vege = (DateTime)read["end_date"];
                        if (!Convert.IsDBNull(read["yacht_id"]))
                            uzenet.yacht_id = (int)read["yacht_id"];
                        if (!Convert.IsDBNull(read["device_id"]))
                            uzenet.device_id = (int)read["device_id"];
                        uzenet.keletkezett = (DateTime)read["date"];
                        if (!Convert.IsDBNull(read["accept"]))
                        {
                            switch ((int)read["accept"])
                            {
                                case 0:
                                    uzenet.elfogadvabl = false;
                                    uzenet.elfogadvastr = "Elutasítva!";
                                    break;
                                case 1:
                                    uzenet.elfogadvabl = true;
                                    uzenet.elfogadvastr = "Elfogadva!";
                                    break;
                            }
                        }
                        if ((int)read["new"] == 1)
                        {
                            uzenet.NEWbl = true;
                            uzenet.NEWstr = "Új üzenet!";
                        }
                        else
                            uzenet.NEWbl = false;
                        if ((int)read["back"] == 1)
                        {
                            uzenet.BlVissza = true;
                            uzenet.StrVissza = "Visszajelzés!";
                        }
                        else
                            uzenet.BlVissza = false;
                        uzenet.price = (int)read["price"];
                        uzenetek.Add(uzenet);
                    }
                }
                YachtName(uzenetek);
                MysqlFelhasznaloName(uzenetek);
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

        /// <summary>
        /// Üzenetek kilistázása
        /// </summary>
        /// <param name="id">Felhasználó id</param>
        /// <returns></returns>
        public List<Message> MysqlUserMessages()
        {
            List<Message> uzenetek = new List<Message>();
            try
            {
                string query = "SELECT enMessage.*, enYacht_Club_Tag.* FROM enMessage INNER JOIN enYacht_Club_Tag ON enYacht_Club_Tag.member_id = enMessage.sender WHERE addressee = ?addressee OR sender = ?sender;";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.Parameters.Add("?addressee", MySqlDbType.Int16).Value = Globals.User.member_id;
                    cmd.Parameters.Add("?sender", MySqlDbType.Int16).Value = Globals.User.member_id;
                    MySqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        Message uzenet = new Message();
                        uzenet.uzenet_id = (int)read["message_id"];
                        uzenet.cimzett_id = (int)read["addressee"];
                        uzenet.felado_id = (int)read["sender"];
                        if (!Convert.IsDBNull(read["from_port"]))
                            uzenet.honnan_id = (int)read["from_port"];
                        if (!Convert.IsDBNull(read["to_port"]))
                            uzenet.hova_id = (int)read["to_port"];
                        uzenet.kezdete = (DateTime)read["start_date"];
                        uzenet.vege = (DateTime)read["end_date"];
                        if (!Convert.IsDBNull(read["yacht_id"]))
                            uzenet.yacht_id = (int)read["yacht_id"];
                        if (!Convert.IsDBNull(read["device_id"]))
                            uzenet.device_id = (int)read["device_id"];
                        uzenet.keletkezett = (DateTime)read["date"];
                        if (!Convert.IsDBNull(read["accept"]))
                        {
                            switch ((int)read["accept"])
                            {
                                case 0:
                                    uzenet.elfogadvabl = false;
                                    uzenet.elfogadvastr = "Elutasítva!";
                                    break;
                                case 1:
                                    uzenet.elfogadvabl = true;
                                    uzenet.elfogadvastr = "Elfogadva!";
                                    break;
                            }
                        }
                        if ((int)read["new"] == 1)
                        {
                            uzenet.NEWbl = true;
                            uzenet.NEWstr = "Új üzenet!";
                        } else
                            uzenet.NEWbl = false;
                        if ((int)read["back"] == 1)
                        {
                            uzenet.BlVissza = true;
                            uzenet.StrVissza = "Visszajelzés!";
                        }
                        else
                            uzenet.BlVissza = false;
                        uzenet.price = (int)read["price"];
                        uzenetek.Add(uzenet);
                    }
                }
                YachtName(uzenetek);
                MysqlFelhasznaloName(uzenetek);
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

        /// <summary>
        /// Új üzenetek kilistázása
        /// </summary>
        /// <param name="id">felhasználó id</param>
        /// <returns></returns>
        public List<Message> MysqlUserNewMessages()
        {
            List<Message> uzenetek = new List<Message>();
            try
            {
                string query = "SELECT enMessage.*, enYacht_Club_Tag.* FROM enMessage INNER JOIN enYacht_Club_Tag ON enYacht_Club_Tag.member_id = enMessage.sender WHERE ( addressee = ?addressee AND new = 1 ) OR ( sender = ?sender AND back = 1 );";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.Parameters.Add("?addressee", MySqlDbType.Int16).Value = Globals.User.member_id;
                    cmd.Parameters.Add("?sender", MySqlDbType.Int16).Value = Globals.User.member_id;
                    MySqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        Message uzenet = new Message();
                        uzenet.uzenet_id = (int)read["message_id"];
                        uzenet.cimzett_id = (int)read["addressee"];
                        uzenet.felado_id = (int)read["sender"];
                        if (!Convert.IsDBNull(read["from_port"]))
                            uzenet.honnan_id = (int)read["from_port"];
                        if (!Convert.IsDBNull(read["to_port"]))
                            uzenet.hova_id = (int)read["to_port"];
                        uzenet.kezdete = (DateTime)read["start_date"];
                        uzenet.vege = (DateTime)read["end_date"];
                        if (!Convert.IsDBNull(read["yacht_id"]))
                            uzenet.yacht_id = (int)read["yacht_id"];
                        if (!Convert.IsDBNull(read["device_id"]))
                            uzenet.device_id = (int)read["device_id"];
                        uzenet.keletkezett = (DateTime)read["date"];
                        if (!Convert.IsDBNull(read["accept"]))
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
                        if ((int)read["back"] == 1)
                        {
                            uzenet.BlVissza = true;
                            uzenet.StrVissza = "Visszajelzés!";
                        }
                        else
                            uzenet.BlVissza = false;
                        uzenet.price = (int)read["price"];
                        uzenetek.Add(uzenet);
                    }
                }
                YachtName(uzenetek);
                MysqlFelhasznaloName(uzenetek);
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
        /// <summary>
        /// Yacht nevek hozzáadása a listához
        /// Azért külön van mert valamioknál fogva hibát adot a mysql amokor összeakartam kötni a táblákat
        /// </summary>
        /// <param name="list"></param>
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

        public void MysqlMessageNewUpdate(int id)
        {
            try
            {
                string query = "UPDATE enMessage SET new = ?new, back = 0 WHERE message_id = ?message_id;";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.Parameters.Add("?message_id", MySqlDbType.Int16).Value = id;
                    cmd.Parameters.Add("?new", MySqlDbType.Int16).Value = 0;
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

        public void MysqlMessageAcceptUpdate(int accept)
        {
            try
            {
                string query = "UPDATE enMessage SET accept = ?accept, back = 1 WHERE message_id = ?message_id;";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.Parameters.Add("?message_id", MySqlDbType.Int16).Value = Globals.selectedMessage.uzenet_id;
                    cmd.Parameters.Add("?accept", MySqlDbType.Int16).Value = accept;
                    cmd.ExecuteNonQuery();
                }
                if (accept == 1)
                {
                    if (Globals.selectedMessage.yacht_id != 0)
                    {
                        MysqlFoglal(Globals.selectedMessage.yacht_id, 1);
                    }
                    else if (Globals.selectedMessage.device_id != 0)
                    {
                        MysqlFoglal(Globals.selectedMessage.device_id, 2);
                    }
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

        private void MysqlFoglal(int id, int YacDev)
        {
            string query = "";
            try
            {
                switch (YacDev)
                {
                    case 1:
                        query = "UPDATE enYacht SET busy = 1, renter = ?renter WHERE yacht_id = ?ID;";
                        break;
                    case 2:
                        query = "UPDATE enDevice SET busy = 1, renter = ?renter WHERE device_id = ?ID;";
                        break;
                }
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.Parameters.Add("?ID", MySqlDbType.Int16).Value = id;
                    cmd.Parameters.Add("?renter", MySqlDbType.Int16).Value = Globals.selectedMessage.felado_id;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error in deleting mysql row. Error: " + ex.Message);
            }
        }

        /// <summary>
        /// üzenet törlése
        /// </summary>
        /// <param name="id">üzenet id</param>
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
        /// <summary>
        /// Felhasználói adatok lekérdezése
        /// </summary>
        /// <param name="id">felhasználó id</param>
        /// <returns>felhasználó</returns>
        public Felhasznalo MysqlFelhasznaloData(int id)
        {
            Felhasznalo user = new Felhasznalo();
            try
            {
                string query = "SELECT * FROM enYacht_Club_Tag INNER JOIN enzipcode USING(zip_code) WHERE member_id = ?member_id;";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.Parameters.Add("?member_id", MySqlDbType.Int32).Value = id;
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
                        { user.kep = MysqlGeneral.ByteToImage((byte[])read["image"]); }
                        catch (Exception)
                        { user.kep = null; }
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
            return user;
        }

        private void MysqlFelhasznaloName(List<Message> list)
        {
            try
            {
                string query = "SELECT first_name, last_name FROM enYacht_Club_Tag WHERE member_id = ?member_id;";
                for (int i = 0; i < list.Count; i++)
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                    {
                        cmd.Parameters.Add("?member_id", MySqlDbType.Int16).Value = list[i].cimzett_id;
                        MySqlDataReader read = cmd.ExecuteReader();
                        while (read.Read())
                            list[i].cimzett_nev = read["first_name"].ToString() + " " + read["last_name"].ToString();
                    }
                    using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                    {
                        cmd.Parameters.Add("?member_id", MySqlDbType.Int16).Value = list[i].felado_id;
                        MySqlDataReader read = cmd.ExecuteReader();
                        while (read.Read())
                            list[i].felado_nev = read["first_name"].ToString() + " " + read["last_name"].ToString();
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error in adding mysql row. Error: " + ex.Message);
            }
        }

        public string MysqlPortName(int id)
        {
            try
            {
                string query = "SELECT name FROM enPort WHERE port_id = ?port_id;";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.Parameters.Add("?port_id", MySqlDbType.Int16).Value = id;
                    return cmd.ExecuteScalar().ToString();
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
            return "Nincs!";
        }
    }
}
