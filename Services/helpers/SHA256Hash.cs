using System;
using System.Text;
using System.Security.Cryptography;

namespace services.helpers {
    public static class SHA256Hash {
        public static string Compute(string input){
            byte[] hashed = hash(input);
            return stringify(hashed);
        }
        private static byte[] hash(string input){
            var inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashed = null;
            using (SHA256 alg = SHA256.Create())
            {
                hashed = alg.ComputeHash(inputBytes);
            }
            return hashed;
        }

        private static string stringify(byte[] input){
            StringBuilder builder = new StringBuilder();
            foreach (byte b in input)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }

    }
}