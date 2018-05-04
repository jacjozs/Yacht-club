using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.IO;
using System;
using System.Windows.Media.Imaging;


namespace Yacht_club.Database
{
    class MysqlYacht
    {
        /// <summary>
        /// Új yacht beírása az adatbázisba
        /// </summary>
        /// <param name="yacht"></param>
        /// <returns></returns>
        public bool MysqlAddYacht(Yacht yacht)
        {
            try
            {
                string query = "INSERT INTO enYacht(yacht_id, name, producer, ower, seats, width, lenght, dive, speed) VALUES (?yacht_id, ?name, ?producer, ?ower, ?seats, ?width, ?lenght, ?dive, ?speed);";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.Parameters.Add("?yacht_id", MySqlDbType.Int16).Value = MysqlGeneral.MysqlNextId("enYacht", "yacht_id");
                    cmd.Parameters.Add("?name", MySqlDbType.VarChar).Value = yacht.nev;
                    cmd.Parameters.Add("?producer", MySqlDbType.VarChar).Value = yacht.gyarto;
                    cmd.Parameters.Add("?ower", MySqlDbType.Int16).Value = yacht.member_id;
                    cmd.Parameters.Add("?seats", MySqlDbType.Int16).Value = yacht.ferohely;
                    cmd.Parameters.Add("?width", MySqlDbType.Float).Value = yacht.szeles;
                    cmd.Parameters.Add("?lenght", MySqlDbType.Float).Value = yacht.hossz;
                    cmd.Parameters.Add("?dive", MySqlDbType.Float).Value = yacht.merules;
                    cmd.Parameters.Add("?speed", MySqlDbType.Int16).Value = yacht.sebesseg;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error in adding mysql row. Error: " + ex.Message);
                return false;
            }
            finally
            {
                Globals.connect.Close();
            }
            return true;
        }
        /// <summary>
        /// teljesnév és id kigyüjtése és egy "map"-ban való eltárolása 
        /// teljesnév a kulcs!
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> MysqlYachtLoginName()
        {
            string full_name = "";
            Dictionary<int, string> login = new Dictionary<int, string>();
            try
            {
                string query = "SELECT first_name, last_name, member_id FROM enYacht_Club_Tag;";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    MySqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        full_name += read["first_name"].ToString() + " " + read["last_name"].ToString();
                        login.Add((int)read["member_id"], full_name);
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
        /// Yacht törlés yacht_id alapján
        /// </summary>
        /// <param name="id">yacht_id</param>
        /// <returns></returns>
        public bool MysqlDeleteYacht(int id)
        {
            try
            {
                string query = "UPDATE enYacht SET hide = 1 WHERE yacht_id = ?yacht_id;";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.Parameters.Add("?yacht_id", MySqlDbType.Int16).Value = id;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error in adding mysql row. Error: " + ex.Message);
                return false;
            }
            finally
            {
                Globals.connect.Close();
            }
            return true;
        }
        /// <summary>
        /// Az összes yacht kigyüjtése listába
        /// </summary>
        /// <returns></returns>
        public List<Yacht> MysqlYachtAll()
        {
            string full_name;
            List<Yacht> Yachts = new List<Yacht>();
            try
            {
                string query = "SELECT enPort.name AS port_name, enYacht.*, enYacht_Club_Tag.*  FROM enYacht INNER JOIN enYacht_Club_Tag ON enYacht_Club_Tag.member_id = enYacht.ower INNER JOIN enPort USING(port_id) WHERE hide = 0;";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    MySqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        Yacht yacht = new Yacht();
                        yacht.berelheto = false;
                        yacht.blfoglalt = false;
                        yacht.strfoglalt = "Szabad";
                        yacht.id = (int)read["yacht_id"];
                        yacht.nev = read["name"].ToString();
                        yacht.gyarto = read["producer"].ToString();
                        full_name = read["first_name"].ToString();
                        full_name += " " + read["last_name"].ToString();
                        yacht.full_name = full_name;
                        yacht.member_id = (int)read["ower"];
                        if (read["renter"].ToString() == "")
                        {
                            yacht.berlo_full_name = "Nincs bérbe adva";
                            yacht.berlo_id = 0;
                        }
                        else
                            yacht.berlo_id = (int)read["renter"];
                        yacht.ferohely = (int)read["seats"];
                        if ((int)read["hire"] == 1)
                            yacht.berelheto = true;
                        if ((int)read["busy"] == 1)
                        {
                            yacht.blfoglalt = true;
                            yacht.strfoglalt = "Foglalt";
                        }
                        if (read["daly_price"].ToString() == "")
                            yacht.napi_ar = 0;
                        else yacht.napi_ar = (int)read["daly_price"];
                        yacht.szeles = (float)read["width"];
                        yacht.hossz = (float)read["lenght"];
                        yacht.merules = (float)read["dive"];
                        yacht.sebesseg = (int)read["speed"];
                        yacht.kikoto = read["port_name"].ToString();
                        yacht.kikoto_id = (int)read["port_id"];
                        Yachts.Add(yacht);
                    }
                }
                Yachts = MysqlMemberFullname(Yachts);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error in adding mysql row. Error: " + ex.Message);
            }
            finally
            {
                Globals.connect.Close();
            }
            return Yachts;
        }
        /// <summary>
        /// Yachtok kigyüjtése member_id alapján
        /// </summary>
        /// <param name="id">member_id</param>
        /// <returns></returns>
        public List<Yacht> MysqlYachts(int id)
        {
            string full_name;
            List<Yacht> Yachts = new List<Yacht>();
            try
            {
                string query = "SELECT enPort.name AS port_name, enYacht.*, enYacht_Club_Tag.* FROM enYacht INNER JOIN enYacht_Club_Tag ON enYacht_Club_Tag.member_id = enYacht.ower INNER JOIN enPort USING(port_id) WHERE ower = ?ower AND hide = 0;";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.Parameters.Add("?ower", MySqlDbType.Int16).Value = id;
                    MySqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        Yacht yacht = new Yacht();
                        yacht.member_id = id;
                        yacht.berelheto = false;
                        yacht.blfoglalt = false;
                        yacht.strfoglalt = "Szabad";
                        yacht.id = (int)read["yacht_id"];
                        yacht.nev = read["name"].ToString();
                        yacht.gyarto = read["producer"].ToString();
                        yacht.member_id = id;
                        full_name = read["first_name"].ToString();
                        full_name += " " + read["last_name"].ToString();
                        yacht.full_name = full_name;
                        if (read["renter"].ToString() == "")
                        {
                            yacht.berlo_full_name = "Nincs bérbe adva";
                            yacht.berlo_id = 0;
                        }
                        else
                            yacht.berlo_id = (int)read["renter"];
                        yacht.ferohely = (int)read["seats"];
                        if ((int)read["hire"] == 1)
                            yacht.berelheto = true;
                        if ((int)read["busy"] == 1)
                        {
                            yacht.blfoglalt = true;
                            yacht.strfoglalt = "Foglalt";
                        }
                        if (read["daly_price"].ToString() == "")
                            yacht.napi_ar = 0;
                        else yacht.napi_ar = (int)read["daly_price"];
                        yacht.szeles = (float)read["width"];
                        yacht.hossz = (float)read["lenght"];
                        yacht.merules = (float)read["dive"];
                        yacht.sebesseg = (int)read["speed"];
                        yacht.kikoto = read["port_name"].ToString();
                        yacht.kikoto_id = (int)read["port_id"];
                        Yachts.Add(yacht);
                    }
                }
                Yachts = MysqlMemberFullname(Yachts);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error in adding mysql row. Error: " + ex.Message);
            }
            finally
            {
                Globals.connect.Close();
            }
            return Yachts;
        }
        /// <summary>
        /// Bérelhető valamint szabad yachtok kigyüjtése
        /// </summary>
        /// <param name="id">member_id</param>
        /// <returns></returns>
        public List<Yacht> MysqlYachtsBerles(int id)
        {
            string full_name;
            List<Yacht> Yachts = new List<Yacht>();
            try
            {
                string query = "SELECT enPort.name AS port_name, enYacht.*, enYacht_Club_Tag.* FROM enYacht INNER JOIN enYacht_Club_Tag ON enYacht_Club_Tag.member_id = enYacht.ower INNER JOIN enPort USING(port_id) WHERE ower != ?ower AND busy = 0 AND hire = 1 AND hide = 0;";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.Parameters.Add("?ower", MySqlDbType.Int16).Value = id;
                    MySqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        Yacht yacht = new Yacht();
                        yacht.berelheto = false;
                        yacht.blfoglalt = false;
                        yacht.strfoglalt = "Szabad";
                        yacht.id = (int)read["yacht_id"];
                        yacht.nev = read["name"].ToString();
                        yacht.gyarto = read["producer"].ToString();
                        yacht.member_id = (int)read["ower"];
                        full_name = read["first_name"].ToString();
                        full_name += " " + read["last_name"].ToString();
                        yacht.full_name = full_name;
                        if (read["renter"].ToString() == "")
                        {
                            yacht.berlo_full_name = "Nincs bérbe adva";
                            yacht.berlo_id = 0;
                        }
                        else
                            yacht.berlo_id = (int)read["renter"];
                        yacht.ferohely = (int)read["seats"];
                        if ((int)read["hire"] == 1)
                            yacht.berelheto = true;
                        if ((int)read["busy"] == 1)
                        {
                            yacht.blfoglalt = true;
                            yacht.strfoglalt = "Foglalt";
                        }
                        if (read["daly_price"].ToString() == "")
                            yacht.napi_ar = 0;
                        else yacht.napi_ar = (int)read["daly_price"];
                        yacht.szeles = (float)read["width"];
                        yacht.hossz = (float)read["lenght"];
                        yacht.merules = (float)read["dive"];
                        yacht.sebesseg = (int)read["speed"];
                        yacht.kikoto = read["port_name"].ToString();
                        yacht.kikoto_id = (int)read["port_id"];
                        Yachts.Add(yacht);
                    }
                }

                Yachts = MysqlMemberFullname(Yachts);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error in adding mysql row. Error: " + ex.Message);
            }
            finally
            {
                Globals.connect.Close();
            }
            return Yachts;
        }
        /// <summary>
        /// teljes név összeállitása member_id alapján
        /// </summary>
        /// <param name="id">member_id</param>
        /// <returns></returns>
        private List<Yacht> MysqlMemberFullname(List<Yacht> yachts)
        {
            string full_name = "";
            try
            {
                string query = "SELECT first_name, last_name FROM enYacht_Club_Tag WHERE member_id = ?member_id;";
                for (int i = 0; i < yachts.Count; i++)
                {
                    if (yachts[i].berlo_id == 0) continue;
                    using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                    {
                        cmd.Parameters.Add("?member_id", MySqlDbType.Int16).Value = yachts[i].member_id;
                        MySqlDataReader read = cmd.ExecuteReader();
                        while (read.Read())
                            full_name += read["first_name"].ToString() + " " + read["last_name"].ToString();
                        yachts[i].berlo_full_name = full_name;
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error in adding mysql row. Error: " + ex.Message);
            }
            return yachts;
        }

        /// <summary>
        /// Kikeresi az adatbázisból egy yachtnak a képét
        /// </summary>
        /// <param name="id">yacht id</param>
        /// <returns>yacht kép</returns>
        public BitmapImage MysqlYachtImage(int id)
        {
            BitmapImage image = null;
            try
            {
                string query = "SELECT Image FROM enYacht WHERE yacht_id = ?yacht_id;";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.Parameters.Add("?yacht_id", MySqlDbType.Int16).Value = id;
                    try
                    { image = MysqlGeneral.ByteToImage((byte[])cmd.ExecuteScalar()); }
                    catch (Exception)
                    { image = null; }
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
            return image;
        }
        /// <summary>
        /// Yacht adatok frissitése az adatbázisba
        /// </summary>
        /// <param name="UpdateYacht"></param>
        public void MysqlUpdateYacht(Yacht UpdateYacht)
        {
            try
            {
                string query = "UPDATE enYacht SET name = ?name, producer = ?producer, image = ?image, seats = ?seats, hire = ?hire, busy = ?busy, daly_price = ?daly_price, width = ?width, lenght = ?lenght, dive = ?dive, speed = ?speed, port_id = ?port_id WHERE yacht_id = ?yacht_id;";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.Parameters.Add("?yacht_id", MySqlDbType.Int16).Value = UpdateYacht.id;
                    cmd.Parameters.Add("?name", MySqlDbType.VarChar).Value = UpdateYacht.nev;
                    cmd.Parameters.Add("?producer", MySqlDbType.VarChar).Value = UpdateYacht.gyarto;
                    if (UpdateYacht.kep != null)
                        cmd.Parameters.Add("?image", MySqlDbType.LongBlob).Value = MysqlGeneral.ImageToByte(UpdateYacht.kep);
                    else cmd.Parameters.Add("?image", MySqlDbType.LongBlob).Value = DBNull.Value;
                    cmd.Parameters.Add("?seats", MySqlDbType.Int16).Value = UpdateYacht.ferohely;
                    cmd.Parameters.Add("?hire", MySqlDbType.Bit).Value = UpdateYacht.berelheto;
                    cmd.Parameters.Add("?busy", MySqlDbType.Bit).Value = UpdateYacht.blfoglalt;
                    cmd.Parameters.Add("?daly_price", MySqlDbType.Int16).Value = UpdateYacht.napi_ar;
                    cmd.Parameters.Add("?width", MySqlDbType.Float).Value = UpdateYacht.szeles;
                    cmd.Parameters.Add("?lenght", MySqlDbType.Float).Value = UpdateYacht.hossz;
                    cmd.Parameters.Add("?dive", MySqlDbType.Float).Value = UpdateYacht.merules;
                    cmd.Parameters.Add("?speed", MySqlDbType.Int16).Value = UpdateYacht.sebesseg;
                    cmd.Parameters.Add("?port_id", MySqlDbType.Int16).Value = UpdateYacht.kikoto_id;
                    cmd.ExecuteNonQuery();
                }

            }
            catch (MySqlException e)
            {
                Globals.log = "Sikertelen módosítás!";
                MessageBox.Show(e.ToString(), e.ToString(), MessageBoxButton.OK);
            }
            finally
            {
                Globals.connect.Close();
            }
        }

        public Yacht MysqlYachtSelect(int id)
        {
            string full_name;
            Yacht yacht = new Yacht();
            try
            {
                string query = "SELECT enPort.name AS port_name, enYacht.*, enYacht_Club_Tag.* FROM enYacht INNER JOIN enYacht_Club_Tag ON enYacht_Club_Tag.member_id = enYacht.ower INNER JOIN enPort USING(port_id) WHERE yacht_id = ?yacht_id AND hide = 0;";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.Parameters.Add("?yacht_id", MySqlDbType.Int16).Value = id;
                    MySqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        yacht.member_id = id;
                        yacht.berelheto = false;
                        yacht.blfoglalt = false;
                        yacht.strfoglalt = "Szabad";
                        yacht.id = (int)read["yacht_id"];
                        yacht.nev = read["name"].ToString();
                        yacht.gyarto = read["producer"].ToString();
                        full_name = read["first_name"].ToString();
                        full_name += " " + read["last_name"].ToString();
                        yacht.full_name = full_name;
                        if (read["renter"].ToString() == "")
                        {
                            yacht.berlo_full_name = "Nincs bérbe adva";
                            yacht.berlo_id = 0;
                        }
                        else
                            yacht.berlo_id = (int)read["renter"];
                        yacht.ferohely = (int)read["seats"];
                        if ((int)read["hire"] == 1)
                            yacht.berelheto = true;
                        if ((int)read["busy"] == 1)
                        {
                            yacht.blfoglalt = true;
                            yacht.strfoglalt = "Foglalt";
                        }
                        if (read["daly_price"].ToString() == "")
                            yacht.napi_ar = 0;
                        else yacht.napi_ar = (int)read["daly_price"];
                        yacht.szeles = (float)read["width"];
                        yacht.hossz = (float)read["lenght"];
                        yacht.merules = (float)read["dive"];
                        yacht.sebesseg = (int)read["speed"];
                        yacht.kikoto = read["port_name"].ToString();
                        yacht.kikoto_id = (int)read["port_id"];
                    }
                }
                yacht.berlo_full_name = MysqlMemberSelectFullname(yacht.berlo_id);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error in adding mysql row. Error: " + ex.Message);
            }
            finally
            {
                Globals.connect.Close();
            }
            return yacht;
        }
        
        private string MysqlMemberSelectFullname(int id)
        {
            string full_name = "";
            try
            {
                string query = "SELECT first_name, last_name FROM enYacht_Club_Tag WHERE member_id = ?member_id;";
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.Parameters.Add("?member_id", MySqlDbType.Int16).Value = id;
                    MySqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                        full_name += read["first_name"].ToString() + " " + read["last_name"].ToString();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error in adding mysql row. Error: " + ex.Message);
            }
            return full_name;
        }
    }
}