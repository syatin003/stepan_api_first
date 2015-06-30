using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace stepan_api.Models
{   // Generate Hash string according to its parameter
    public static class Hash
    {
        #region Generate hash of device id Or ip address
        public static string Hashing(string password)
        {
            SHA1 sha = SHA1.Create();
            byte[] hashData = sha.ComputeHash(Encoding.Default.GetBytes(password));
            StringBuilder value = new StringBuilder();
            for (int i = 0; i < hashData.Length; i++)
            {
                value.Append(hashData[i].ToString());
            }
            return Convert.ToBase64String(hashData);
        }
        #endregion
    }
}