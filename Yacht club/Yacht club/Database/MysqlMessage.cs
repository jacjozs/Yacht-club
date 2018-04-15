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
        /// <summary>
        /// Üzenetek kilistázása
        /// </summary>
        /// <param name="id">Felhasználó id</param>
        /// <returns></returns>
        public List<Message> MysqlUserMessages(int id)
        {
            List<Message> uzenetek = new List<Message>();
            try
            {
                string query = "SELECT enMessage.*, enYacht_Club_Tag.* FROM enMessage INNER JOIN enYacht_Club_Tag ON enYacht_Club_Tag.member_id = enMessage.sender WHERE addressee = ?addressee OR sender = ?sender;";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.Parameters.Add("?addressee", MySqlDbType.Int16).Value = id;
                    cmd.Parameters.Add("?sender", MySqlDbType.Int16).Value = Globals.User.member_id;
                    MySqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        Message uzenet = new Message();
                        uzenet.uzenet_id = (int)read["message_id"];
                        uzenet.cimzett_id = id;
                        uzenet.cimzett_nev = Globals.User.teljes_nev;
                        uzenet.felado_id = (int)read["sender"];
                        uzenet.felado_nev = read["first_name"].ToString() + " " + read["last_name"].ToString();
                        if (!Convert.IsDBNull(read["from_port"]))
                            uzenet.honnan_id = (int)read["from_port"];
                        if (!Convert.IsDBNull(read["to_port"]))
                            uzenet.honnan_id = (int)read["to_port"];
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
        /// <summary>
        /// Új üzenetek kilistázása
        /// </summary>
        /// <param name="id">felhasználó id</param>
        /// <returns></returns>
        public List<Message> MysqlUserNewMessages(int id)
        {
            List<Message> uzenetek = new List<Message>();
            try
            {
                string query = "SELECT enMessage.*, enYacht_Club_Tag.* FROM enMessage INNER JOIN enYacht_Club_Tag ON enYacht_Club_Tag.member_id = enMessage.sender WHERE ( addressee = ?addressee AND new = 1 ) OR ( sender = ?sender AND back = 1 );";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.Parameters.Add("?addressee", MySqlDbType.Int16).Value = id;
                    cmd.Parameters.Add("?sender", MySqlDbType.Int16).Value = Globals.User.member_id;
                    MySqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        Message uzenet = new Message();
                        uzenet.uzenet_id = (int)read["message_id"];
                        uzenet.cimzett_id = id;
                        uzenet.cimzett_nev = Globals.User.teljes_nev;
                        uzenet.felado_id = (int)read["sender"];
                        uzenet.felado_nev = read["first_name"].ToString() + " " + read["last_name"].ToString();
                        if (!Convert.IsDBNull(read["from_port"]))
                            uzenet.honnan_id = (int)read["from_port"];
                        if (!Convert.IsDBNull(read["to_port"]))
                            uzenet.honnan_id = (int)read["to_port"];
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
                        MysqlFoglalt(Globals.selectedMessage.yacht_id, 1);
                    }
                    else if (Globals.selectedMessage.device_id != 0)
                    {
                        MysqlFoglalt(Globals.selectedMessage.device_id, 2);
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

        private void MysqlFoglalt(int id, int YacDev)
        {
            string query = "";
            try
            {
                switch (YacDev)
                {
                    case 1:
                        query = "UPDATE enYacht SET busy = 1 WHERE yacht_id = ?ID;";
                        break;
                    case 2:
                        query = "UPDATE enDevice SET busy = 1 WHERE device_id = ?ID;";
                        break;
                }
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.Parameters.Add("?ID", MySqlDbType.Int16).Value = id;
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
                        { user.kep = ByteToImage((byte[])read["image"]); }
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

        /// <summary>
        /// Az átvett byte tömböt képé alakitja
        /// </summary>
        /// <param name="imageByte"></param>
        /// <returns></returns>
        private BitmapImage ByteToImage(byte[] imageByte)
        {
            MemoryStream stream = new MemoryStream(imageByte);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = stream;
            image.EndInit();
            return image;
        }
    }
}
