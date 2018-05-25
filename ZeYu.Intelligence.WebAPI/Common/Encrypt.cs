using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COCOPASS.Helper.NetCoreHelper
{
    public class Encrypt
    {
        public static string MD5(string str)
        {
            string ret = string.Empty;
            byte[] md5Bt = System.Text.Encoding.UTF8.GetBytes(str);
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                var hash = md5.ComputeHash(md5Bt);
                string hex = BitConverter.ToString(hash);
                ret = hex.Replace("-", "");
            }
            return ret;
        }

        /// <summary>
        /// System.Security.Cryptography.HMACSHA256
        /// 计算的值用于存储用户密码等 UTF-8编码
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static string ToHMACSHA256HashString(string txt)
        {
            byte[] passwordAndSaltBytes = System.Text.Encoding.UTF8.GetBytes(txt);
            System.Security.Cryptography.HMACSHA256 hmacSHA256 = new System.Security.Cryptography.HMACSHA256(passwordAndSaltBytes);
            byte[] hashBytes = hmacSHA256.ComputeHash(passwordAndSaltBytes);
            string hashString = Convert.ToBase64String(hashBytes);
            return hashString;
        }

        public static byte Xor(byte[] data)
        {
            byte checkValue = 0;
            if (data.Length < 3)
            {
                checkValue = data[0];
            }
            else
            {
                int result = data[0] ^ data[1];
                for (int g = 2; g < data.Length - 2; g++)
                {
                    result = result ^ data[g];
                }
                checkValue = (byte)result;
            }
            return checkValue;
        }
        public static sbyte Xor(sbyte[] data)
        {
            sbyte checkValue = 0;
            if (data.Length < 3)
            {
                checkValue = data[0];
            }
            else
            {
                int result = data[0] ^ data[1];
                for (int g = 2; g < data.Length - 2; g++)
                {
                    result = result ^ data[g];
                }
                checkValue = (sbyte)result;
            }
            return checkValue;
        }
    }
}
