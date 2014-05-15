using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BaladasWS.Model
{
    public class StringMD5
    {
        public static string ComputeHash(string value)
        {
            if (string.IsNullOrEmpty(value)) { return string.Empty; }

            MD5 md5 = MD5.Create();

            byte[] inputBytes = Encoding.UTF8.GetBytes(value);
            byte[] outputBytes = md5.ComputeHash(inputBytes);

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < outputBytes.Length; i++)
            {
                builder.Append(outputBytes[i].ToString("X2"));
            }

            return builder.ToString();
        }


        
    }
}