using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace Yacht_club.Database
{
    class MysqlYacht
    {
        public bool MysqlAddYacht(Yacht yacht)
        {
            try
            {
                string query;
                if (yacht.kep != null)
                    query = "INSERT INTO enYacht(yacht_id, name, type, image, ower, seats, width, lenght, height, weight) VALUES (?yacht_id, ?name, ?type, ?image, ?ower, ?seats, ?width, ?lenght, ?height, ?weight);";
                else query = "INSERT INTO enYacht(yacht_id, name, type, ower, seats, width, lenght, height, weight) VALUES (?yacht_id, ?name, ?type, ?ower, ?seats, ?width, ?lenght, ?height, ?weight);";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.Parameters.Add("?yacht_id", MySqlDbType.Int16).Value = MysqlNextId("enYacht", "yacht_id");
                    cmd.Parameters.Add("?name", MySqlDbType.VarChar).Value = yacht.nev;
                    cmd.Parameters.Add("?type", MySqlDbType.VarChar).Value = yacht.tipus;
                    if (yacht.kep != null)
                        cmd.Parameters.Add("?image", MySqlDbType.Blob).Value = ImageToByte(yacht.kep);
                    cmd.Parameters.Add("?ower", MySqlDbType.Int16).Value = yacht.member_id;
                    cmd.Parameters.Add("?seats", MySqlDbType.Int16).Value = yacht.ferohely;
                    cmd.Parameters.Add("?width", MySqlDbType.Int16).Value = yacht.szeles;
                    cmd.Parameters.Add("?lenght", MySqlDbType.Int16).Value = yacht.hossz;
                    cmd.Parameters.Add("?height", MySqlDbType.Int16).Value = yacht.magas;
                    cmd.Parameters.Add("?weight", MySqlDbType.Int16).Value = yacht.suly;
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
        /// Kigyüjti a felhasználok login nevét és id-jét valamint login név szerin kulcsoza
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, int> MysqlYachtLoginName()
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
                        login.Add(read["login_name"].ToString(), (int)read["login_id"]);
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

        public bool MysqlDeleteYacht(int id)
        {
            try
            {
                string query = "DELETE FROM enYacht WHERE yacht_id = ?yacht_id;";
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

        public List<Yacht> MysqlYachtAll()
        {
            string full_name;
            List<Yacht> Yachts = new List<Yacht>();
            try
            {
                string query = "SELECT enPort.name AS port_name, enYacht.*, enYacht_Club_Tag.*  FROM enYacht INNER JOIN enYacht_Club_Tag ON enYacht_Club_Tag.member_id = enYacht.ower INNER JOIN enPort USING(port_id);";
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
                        yacht.tipus = read["type"].ToString();
                        full_name = read["first_name"].ToString();
                        full_name += " " + read["last_name"].ToString();
                        yacht.full_name = full_name;
                        if (read["renter"].ToString() == "")
                        {
                            yacht.berlo_full_name = "Nincs bérbe adva";
                            yacht.berlo_id = 0;
                        }
                        else
                        {
                            yacht.berlo_id = (int)read["renter"];
                            yacht.berlo_full_name = MysqlMemberFullname(yacht.berlo_id);
                        }
                        yacht.ferohely = (int)read["seats"];
                        if((int)read["hire"] == 1)
                            yacht.berelheto = true;
                        if ((int)read["busy"] == 1)
                        {
                            yacht.blfoglalt = true;
                            yacht.strfoglalt = "Foglalt";
                        }
                        if (read["daly_price"].ToString() == "")
                            yacht.napi_ar = 0;
                        else yacht.napi_ar = (int)read["daly_price"];
                        yacht.szeles = (int)read["width"];
                        yacht.hossz = (int)read["lenght"];
                        yacht.magas = (int)read["height"];
                        yacht.suly = (int)read["weight"];
                        yacht.kikoto = read["port_name"].ToString();
                        yacht.kikoto_id = (int)read["port_id"];
                        Yachts.Add(yacht);
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
            return Yachts;
        }

        public List<Yacht> MysqlYachts(int id)
        {
            string full_name;
            List<Yacht> Yachts = new List<Yacht>();
            try
            {
                string query = "SELECT enPort.name AS port_name, enYacht.*, enYacht_Club_Tag.* FROM enYacht INNER JOIN enYacht_Club_Tag ON enYacht_Club_Tag.member_id = enYacht.ower INNER JOIN enPort USING(port_id) WHERE ower = ?ower;";
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
                        yacht.tipus = read["type"].ToString();
                        full_name = read["first_name"].ToString();
                        full_name += " " + read["last_name"].ToString();
                        yacht.full_name = full_name;
                        if (read["renter"].ToString() == "")
                        {
                            yacht.berlo_full_name = "Nincs bérbe adva";
                            yacht.berlo_id = 0;
                        }
                        else
                        {
                            yacht.berlo_id = (int)read["renter"];
                            yacht.berlo_full_name = MysqlMemberFullname(yacht.berlo_id);
                        }
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
                        yacht.szeles = (int)read["width"];
                        yacht.hossz = (int)read["lenght"];
                        yacht.magas = (int)read["height"];
                        yacht.suly = (int)read["weight"];
                        yacht.kikoto = read["port_name"].ToString();
                        yacht.kikoto_id = (int)read["port_id"];
                        Yachts.Add(yacht);
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
            return Yachts;
        }

        private string MysqlMemberFullname(int id)
        {
            string full_name = "";
            try
            {
                string query = "SELECT first_name, last_name FROM enYacht_Club_Tag WHERE member_id = ?member_id;";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.Parameters.Add("?member_id", MySqlDbType.Int16).Value = id;
                    cmd.ExecuteNonQuery();
                    full_name += cmd.ExecuteReader()["first_name"].ToString() + " " + cmd.ExecuteReader()["last_name"].ToString();
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
            return full_name;
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
