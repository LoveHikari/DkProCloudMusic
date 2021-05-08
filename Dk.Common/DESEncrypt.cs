using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Dk.Common
{
    public class DESEncrypt
    {
        // Token: 0x0600007F RID: 127 RVA: 0x00007DA8 File Offset: 0x00005FA8
        public static string Encrypt(string Text, string sKey)
        {
            if (string.IsNullOrEmpty(Text))
            {
                return "";
            }
            DESCryptoServiceProvider descryptoServiceProvider = new DESCryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(Text);
            descryptoServiceProvider.Key = Encoding.ASCII.GetBytes(DESEncrypt.Md5Hash(sKey));
            descryptoServiceProvider.IV = Encoding.ASCII.GetBytes(DESEncrypt.Md5Hash(sKey));
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, descryptoServiceProvider.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(bytes, 0, bytes.Length);
            cryptoStream.FlushFinalBlock();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in memoryStream.ToArray())
            {
                stringBuilder.AppendFormat("{0:X2}", b);
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string Decrypt(string Text, string sKey)
        {
            if (string.IsNullOrEmpty(Text))
            {
                return Text;
            }
            DESCryptoServiceProvider descryptoServiceProvider = new DESCryptoServiceProvider();
            int num = Text.Length / 2;
            byte[] array = new byte[num];
            for (int i = 0; i < num; i++)
            {
                int num2 = Convert.ToInt32(Text.Substring(i * 2, 2), 16);
                array[i] = (byte)num2;
            }
            descryptoServiceProvider.Key = Encoding.ASCII.GetBytes(DESEncrypt.Md5Hash(sKey));
            descryptoServiceProvider.IV = Encoding.ASCII.GetBytes(DESEncrypt.Md5Hash(sKey));
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, descryptoServiceProvider.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(array, 0, array.Length);
            cryptoStream.FlushFinalBlock();
            return Encoding.UTF8.GetString(memoryStream.ToArray());
        }

        // Token: 0x06000081 RID: 129 RVA: 0x00007F14 File Offset: 0x00006114
        private static string Md5Hash(string str)
        {
            string text = "";
            byte[] array = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(str));
            for (int i = 0; i < array.Length; i++)
            {
                text += string.Format("{0:X2}", array[i]);
            }
            return text.Substring(0, 8);
        }
    }
}