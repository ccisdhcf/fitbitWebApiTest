using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace fitbitWebApiTest.tools
{
    public class stringGen
    {
        private static Random random = new Random();

        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public string RandomString(int minLength, int maxLength)
        {
            int length = random.Next(minLength, maxLength + 1);
            return RandomString(length);
        }
        public string ComputeSha256Hash(string text)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(text));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public string ComputeBase64Url(string text)
        {
            string result = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(text));

            return result;
        }
        public string Base64Encode(string text)
        {
            string result=Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
            return result;

        }
        public string SHA256PlusBase64(string text)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(text));

                string result = WebEncoders.Base64UrlEncode(bytes);
                return result;
            }
        }



    }
}
