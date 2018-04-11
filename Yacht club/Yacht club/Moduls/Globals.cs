using MySql.Data.MySqlClient;
using System;
using System.Windows.Controls;
using System.Security.Cryptography;
using Yacht_club.Moduls;
using System.Text;

namespace Yacht_club
{
    /// <summary>
    /// Globális adatok a könnyeb adat elérésért
    /// </summary>
    public static class Globals
    {
        public static string log = "";
        public static UsingControls.ucLog log_windows = new UsingControls.ucLog();
        public static MySqlConnection connect = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;database=gamfyachtclub");
        public static Main_Yacht_Window Main;
        public static UserControl History;
        public static int Login_Hiba_Code;
        internal static Themes MainTheme;
        internal static Felhasznalo User;
        internal static Yacht selectedYacht;
        internal static Device selectedDevice;
        internal static Message selectedMessage;

        public static void UpdateHistory()
        {
            if (Main != null)
            {
                History = (UserControl)Main.ccWindow_Main.Content;
            }
        }

        /// <summary>
        /// Jelszó kódolás
        /// </summary>
        /// <param name="password">kódolni kivánt szöveg</param>
        /// <returns></returns>
        public static string Encryption(string password)
        {
            string publicKey = "<RSAKeyValue><Modulus>21wEnTU+mcD2w0Lfo1Gv4rtcSWsQJQTNa6gio05AOkV/Er9w3Y13Ddo5wGtjJ19402S71HUeN0vbKILLJdRSES5MHSdJPSVrOqdrll/vLXxDxWs/U0UT1c8u6k/Ogx9hTtZxYwoeYqdhDblof3E75d9n2F0Zvf6iTb4cI7j6fMs=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

            byte[] passwdByte = Encoding.UTF8.GetBytes(password);

            using (var rsa = new RSACryptoServiceProvider(1024))
            {
                try
                {                
                    rsa.FromXmlString(publicKey.ToString());
                    byte[] encryptedData = rsa.Encrypt(passwdByte, true);
                    string base64Encrypted = Convert.ToBase64String(encryptedData);
                    return base64Encrypted;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }

        /// <summary>
        /// Jelszó dekódoló
        /// </summary>
        /// <param name="password">Dekódolni kivánt szöveg</param>
        /// <returns></returns>
        public static string Decryption(string password)
        {
            string privateKey = "<RSAKeyValue><Modulus>21wEnTU+mcD2w0Lfo1Gv4rtcSWsQJQTNa6gio05AOkV/Er9w3Y13Ddo5wGtjJ19402S71HUeN0vbKILLJdRSES5MHSdJPSVrOqdrll/vLXxDxWs/U0UT1c8u6k/Ogx9hTtZxYwoeYqdhDblof3E75d9n2F0Zvf6iTb4cI7j6fMs=</Modulus><Exponent>AQAB</Exponent><P>/aULPE6jd5IkwtWXmReyMUhmI/nfwfkQSyl7tsg2PKdpcxk4mpPZUdEQhHQLvE84w2DhTyYkPHCtq/mMKE3MHw==</P><Q>3WV46X9Arg2l9cxb67KVlNVXyCqc/w+LWt/tbhLJvV2xCF/0rWKPsBJ9MC6cquaqNPxWWEav8RAVbmmGrJt51Q==</Q><DP>8TuZFgBMpBoQcGUoS2goB4st6aVq1FcG0hVgHhUI0GMAfYFNPmbDV3cY2IBt8Oj/uYJYhyhlaj5YTqmGTYbATQ==</DP><DQ>FIoVbZQgrAUYIHWVEYi/187zFd7eMct/Yi7kGBImJStMATrluDAspGkStCWe4zwDDmdam1XzfKnBUzz3AYxrAQ==</DQ><InverseQ>QPU3Tmt8nznSgYZ+5jUo9E0SfjiTu435ihANiHqqjasaUNvOHKumqzuBZ8NRtkUhS6dsOEb8A2ODvy7KswUxyA==</InverseQ><D>cgoRoAUpSVfHMdYXW9nA3dfX75dIamZnwPtFHq80ttagbIe4ToYYCcyUz5NElhiNQSESgS5uCgNWqWXt5PnPu4XmCXx6utco1UVH8HGLahzbAnSy6Cj3iUIQ7Gj+9gQ7PkC434HTtHazmxVgIR5l56ZjoQ8yGNCPZnsdYEmhJWk=</D></RSAKeyValue>";
            using (var rsa = new RSACryptoServiceProvider(1024))
            {
                try
                {
                    rsa.FromXmlString(privateKey);
                    byte[] decryptedBytes = rsa.Decrypt(Convert.FromBase64String(password), true);
                    string decryptedData = Encoding.UTF8.GetString(decryptedBytes);
                    return decryptedData;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }
    }
}
