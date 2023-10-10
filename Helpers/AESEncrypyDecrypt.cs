using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TenantCompany.Helpers
{
    public class AESEncrypyDecrypt
    {
        private readonly string encryptionKey;
        private readonly IConfiguration _configuration;
        public AESEncrypyDecrypt(IConfiguration configuration)
        {
            _configuration = configuration;
            encryptionKey = configuration["EncryptionKey"];
        }

       
        public string Encrypt(string str)
        {
            //string encryptionKey = "[Teknologikol+{[}^&#@!Logistics;eNcRyPt~code[pages(>for<)";
            byte[] clearBytes = Encoding.Unicode.GetBytes(str);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76,
                                                                                            0x9e,0x45,0x7d,0x9f,0x5c,0x2a});
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    str = Convert.ToBase64String(ms.ToArray());
                }
            }

            return str;
        }

        public string Decrypt(string str)
        {
            str = str.Replace(" ", "+");
            //string encryptionKey = "[Teknologikol+{[}^&#@!Logistics;eNcRyPt~code[pages(>for<)";
            byte[] cipherBytes = Convert.FromBase64String(str);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76,
                                                                                            0x9e,0x45,0x7d,0x9f,0x5c,0x2a});
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    str = Encoding.Unicode.GetString(ms.ToArray());
                }
            }

            return str;
        }
    }
}
