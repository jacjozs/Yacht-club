﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.IO;

namespace Yacht_club.Database
{
    class MysqlSetting
    {
        /// <summary>
        /// Felhasználói rekord frissitése
        /// </summary>
        /// <param name="UpdateUser">A frissteni kivánt felhasználó</param>
        public void MysqlUpdateUser(Felhasznalo UpdateUser)
        {
            try
            {
                string query = "UPDATE enYacht_Club_Tag SET nickname = ?nickname, last_name = ?last_name, first_name = ?first_name, birthday = ?birthday, zip_code = ?zip_code, address = ?address, country = ?country, image = ?image WHERE member_id = ?member_id;";
                Globals.connect.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, Globals.connect))
                {
                    cmd.Parameters.Add("?nickname", MySqlDbType.VarChar).Value = UpdateUser.nickname;
                    cmd.Parameters.Add("?last_name", MySqlDbType.VarChar).Value = UpdateUser.kereszt_nev;
                    cmd.Parameters.Add("?first_name", MySqlDbType.VarChar).Value = UpdateUser.veztek_nev;
                    cmd.Parameters.Add("?birthday", MySqlDbType.DateTime).Value = UpdateUser.szuletesdt;
                    cmd.Parameters.Add("?zip_code", MySqlDbType.Int16).Value = UpdateUser.iranyitoszm;
                    cmd.Parameters.Add("?address", MySqlDbType.VarChar).Value = UpdateUser.lakcim;
                    cmd.Parameters.Add("?country", MySqlDbType.VarChar).Value = UpdateUser.orszag;
                    cmd.Parameters.Add("?image", MySqlDbType.Blob).Value = ImageToByte(UpdateUser.kep);
                    cmd.Parameters.Add("?member_id", MySqlDbType.Int16).Value = UpdateUser.member_id;
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception)
            {
                Globals.log = "Sikertelen módosítás!";
            }
            finally
            {
                Globals.connect.Close();
            }
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
    }
}