using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Yacht_club.Database
{
    class MysqlDevice
    {
        /// <summary>
        /// Új szállitóeszköz beírása az adatbázisba
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public int MysqlAddDevice(Device device)
        {
            int id = 0;
            try
            {
                string query = "INSERT INTO enDevice(device_id, type, ower, max_lenght, max_weight) VALUES (?device_id, ?type, ?ower, ?max_lenght, ?max_weight);";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    id = MysqlGeneral.MysqlNextId("enDevice", "device_id");
                    cmd.Parameters.Add("?device_id", MySqlDbType.Int16).Value = id;
                    cmd.Parameters.Add("?type", MySqlDbType.VarChar).Value = device.tipus;
                    cmd.Parameters.Add("?ower", MySqlDbType.Int16).Value = device.member_id;
                    cmd.Parameters.Add("?max_lenght", MySqlDbType.VarChar).Value = device.max_hossz;
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
        /// <summary>
        /// Szállitóeszköz törlése
        /// </summary>
        /// <param name="id">a törölni kivánt szállitóeszköz id-e</param>
        /// <returns></returns>
        public bool MysqlDeleteDevice(int id)
        {
            try
            {
                string query = "DELETE FROM enDevice WHERE device_id = ?device_id;";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.Parameters.Add("?device_id", MySqlDbType.Int16).Value = id;
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
        /// Az összes szállitó eszköz kigyüjtése
        /// </summary>
        /// <returns></returns>
        public List<Device> MysqlDeviceAll()
        {
            string full_name;
            List<Device> Devices = new List<Device>();
            try
            {
                string query = "SELECT enDevice.*, enYacht_Club_Tag.* FROM enDevice INNER JOIN enYacht_Club_Tag ON enYacht_Club_Tag.member_id = enDevice.ower;";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    MySqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        Device device = new Device();
                        device.berelheto = false;
                        device.blfoglalt = false;
                        device.strfoglalt = "Szabad";
                        device.id = (int)read["device_id"];
                        device.tipus = read["type"].ToString();
                        full_name = read["first_name"].ToString();
                        full_name += " " + read["last_name"].ToString();
                        device.full_name = full_name;
                        device.member_id = (int)read["ower"];
                        if (read["renter"].ToString() == "")
                        {
                            device.berlo_full_name = "Nincs bérbe adva";
                            device.berlo_id = 0;
                        }
                        else
                        {
                            device.berlo_id = (int)read["renter"];
                            device.berlo_full_name = MysqlMemberFullname(device.berlo_id);
                        }
                        if ((int)read["hire"] == 1)
                            device.berelheto = true;
                        if ((int)read["busy"] == 1)
                        {
                            device.blfoglalt = true;
                            device.strfoglalt = "Foglalt";
                        }
                        if (read["daly_price"].ToString() == "")
                            device.napi_ar = 0;
                        else device.napi_ar = (int)read["daly_price"];
                        device.max_hossz = read["max_lenght"].ToString();
                        device.max_suly = (int)read["max_weight"];
                        Devices.Add(device);
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
            return Devices;
        }
        /// <summary>
        /// Szállitó eszközök kigyüjtése member_id alapján
        /// </summary>
        /// <param name="id">member_id vagy más néven felhasználó azonosító</param>
        /// <returns></returns>
        public List<Device> MysqlDevices(int id)
        {
            string full_name;
            List<Device> Devices = new List<Device>();
            try
            {
                string query = "SELECT enDevice.*, enYacht_Club_Tag.* FROM enDevice INNER JOIN enYacht_Club_Tag ON enYacht_Club_Tag.member_id = enDevice.ower WHERE ower = ?ower;";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.Parameters.Add("?ower", MySqlDbType.Int16).Value = id;
                    MySqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        Device device = new Device();
                        device.member_id = id;
                        device.berelheto = false;
                        device.blfoglalt = false;
                        device.strfoglalt = "Szabad";
                        device.id = int.Parse(read["device_id"].ToString());
                        device.tipus = read["type"].ToString();
                        full_name = read["first_name"].ToString();
                        full_name += " " + read["last_name"].ToString();
                        device.full_name = full_name;
                        if (read["renter"].ToString() == "")
                        {
                            device.berlo_full_name = "Nincs bérbe adva";
                            device.berlo_id = 0;
                        }
                        else
                        {
                            device.berlo_id = (int)read["renter"];
                            device.berlo_full_name = MysqlMemberFullname(device.berlo_id);
                        }
                        if ((int)read["hire"] == 1)
                            device.berelheto = true;
                        if ((int)read["busy"] == 1)
                        {
                            device.blfoglalt = true;
                            device.strfoglalt = "Foglalt";
                        }
                        if (read["daly_price"].ToString() == "")
                            device.napi_ar = 0;
                        else device.napi_ar = (int)read["daly_price"];
                        device.max_hossz = read["max_lenght"].ToString();
                        device.max_suly = (int)read["max_weight"];
                        Devices.Add(device);
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
            return Devices;
        }
        /// <summary>
        /// Bérelhető valamint szabad szállitó eszközök kigyüjtése
        /// </summary>
        /// <param name="id">bérelni kivánó szeméyl member_id-e</param>
        /// <returns></returns>
        public List<Device> MysqlDevicesBerles(int id)
        {
            string full_name;
            List<Device> Devices = new List<Device>();
            try
            {
                string query = "SELECT enDevice.*, enYacht_Club_Tag.* FROM enDevice INNER JOIN enYacht_Club_Tag ON enYacht_Club_Tag.member_id = enDevice.ower WHERE ower != ?ower AND busy = 0 AND hire = 1;";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.Parameters.Add("?ower", MySqlDbType.Int16).Value = id;
                    MySqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        Device device = new Device();
                        device.berelheto = false;
                        device.blfoglalt = false;
                        device.strfoglalt = "Szabad";
                        device.id = int.Parse(read["device_id"].ToString());
                        device.tipus = read["type"].ToString();
                        full_name = read["first_name"].ToString();
                        full_name += " " + read["last_name"].ToString();
                        device.full_name = full_name;
                        device.member_id = (int)read["ower"];
                        if (read["renter"].ToString() == "")
                        {
                            device.berlo_full_name = "Nincs bérbe adva";
                            device.berlo_id = 0;
                        }
                        else
                        {
                            device.berlo_id = (int)read["renter"];
                            device.berlo_full_name = MysqlMemberFullname(device.berlo_id);
                        }
                        if ((int)read["hire"] == 1)
                            device.berelheto = true;
                        if ((int)read["busy"] == 1)
                        {
                            device.blfoglalt = true;
                            device.strfoglalt = "Foglalt";
                        }
                        if (read["daly_price"].ToString() == "")
                            device.napi_ar = 0;
                        else device.napi_ar = (int)read["daly_price"];
                        device.max_hossz = read["max_lenght"].ToString();
                        device.max_suly = (int)read["max_weight"];
                        Devices.Add(device);
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
            return Devices;
        }
        /// <summary>
        /// Szállitóeszköz adatainak modósítása
        /// </summary>
        /// <param name="UpdateDevice"></param>
        public void MysqlUpdateDevicet(Device UpdateDevice)
        {
            try
            {
                string query = "UPDATE enDevice SET daly_price = ?daly_price, max_lenght=?max_lenght, max_weight=?max_weight, hire=?hire WHERE device_id=?device_id;";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.Parameters.Add("?daly_price", MySqlDbType.Int16).Value = UpdateDevice.napi_ar;
                    cmd.Parameters.Add("?max_lenght", MySqlDbType.Int16).Value = UpdateDevice.max_hossz;
                    cmd.Parameters.Add("?max_weight", MySqlDbType.Int16).Value = UpdateDevice.max_suly;
                    cmd.Parameters.Add("?hire", MySqlDbType.Bit).Value = UpdateDevice.blfoglalt;
                    cmd.Parameters.Add("?device_id", MySqlDbType.Int16).Value = Globals.selectedDevice.id;
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception)
            {
            }
            finally
            {
                Globals.connect.Close();
            }
        }

        /// <summary>
        /// teljes név összeállitása member_id alapján
        /// </summary>
        /// <param name="id">member_id</param>
        /// <returns></returns>
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

        public Device MysqlDeviceSelect(int id)
        {
            string full_name;
            Device device = new Device();
            try
            {
                string query = "SELECT enDevice.*, enYacht_Club_Tag.* FROM enDevice INNER JOIN enYacht_Club_Tag ON enYacht_Club_Tag.member_id = enDevice.ower WHERE device_id = ?device_id;";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.Parameters.Add("?device_id", MySqlDbType.Int16).Value = id;
                    MySqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        device.member_id = id;
                        device.berelheto = false;
                        device.blfoglalt = false;
                        device.strfoglalt = "Szabad";
                        device.id = int.Parse(read["device_id"].ToString());
                        device.tipus = read["type"].ToString();
                        full_name = read["first_name"].ToString();
                        full_name += " " + read["last_name"].ToString();
                        device.full_name = full_name;
                        device.member_id = (int)read["ower"];
                        if (read["renter"].ToString() == "")
                        {
                            device.berlo_full_name = "Nincs bérbe adva";
                            device.berlo_id = 0;
                        }
                        else
                        {
                            device.berlo_id = (int)read["renter"];
                            device.berlo_full_name = MysqlMemberFullname(device.berlo_id);
                        }
                        if ((int)read["hire"] == 1)
                            device.berelheto = true;
                        if ((int)read["busy"] == 1)
                        {
                            device.blfoglalt = true;
                            device.strfoglalt = "Foglalt";
                        }
                        if (read["daly_price"].ToString() == "")
                            device.napi_ar = 0;
                        else device.napi_ar = (int)read["daly_price"];
                        device.max_hossz = read["max_lenght"].ToString();
                        device.max_suly = (int)read["max_weight"];
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
            return device;
        }
    }
}
