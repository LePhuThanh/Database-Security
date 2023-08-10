using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lab04
{
    class DataEncryption
    {
        public static string key = "1712156";
        public static byte[] EncryptStringToBytes_SHA1(string MATKHAU)
        {
            SHA1CryptoServiceProvider Sha1 = new SHA1CryptoServiceProvider();
            byte[] data = Sha1.ComputeHash(Encoding.Default.GetBytes(MATKHAU));
            return data;
        }

        public static byte[] EncryptStringToBytes_Aes256(string plainText)
        {
            byte[] encrypted;
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                byte[] keyBytes = new byte[32];
                byte[] ivBytes = new byte[16];

                System.Text.Encoding.UTF8.GetBytes(key, 0, 1, keyBytes, 0);
                System.Text.Encoding.UTF8.GetBytes(key, 0, 1, ivBytes, 0);

                AesCryptoServiceProvider myAes = new AesCryptoServiceProvider();
                aesAlg.Key = keyBytes;
                aesAlg.IV = ivBytes;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            //.
            return encrypted;
        }

        public static string DecryptStringFromBytes_Aes256(byte[] cipherText)
        {
            string plainText = null;
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                byte[] keyByte = new byte[32];
                byte[] ivByte = new byte[16];

                System.Text.UTF8Encoding.UTF8.GetBytes(key, 0, 1, keyByte, 0);
                System.Text.UTF8Encoding.UTF8.GetBytes(key, 0, 1, ivByte, 0);

                AesCryptoServiceProvider myAes = new AesCryptoServiceProvider();
                aesAlg.Key = keyByte;
                aesAlg.IV = ivByte;


                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plainText = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plainText;
        }

    }
}
