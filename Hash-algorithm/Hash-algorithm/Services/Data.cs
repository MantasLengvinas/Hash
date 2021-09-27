using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hash_algorithm.Services
{
    public class Data
    {
        public List<string> GenerateStrings()
        {
            return new List<string>();
        }

        public string RandomStringGenerator(int l)
        {
            string possibleChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string result = "";

            using(RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] uintB = new byte[sizeof(uint)];

                while(l > 0)
                {
                    rng.GetBytes(uintB);
                    uint number = BitConverter.ToUInt32(uintB, 0);
                    result.Append(possibleChars[(int)(number % (uint)possibleChars.Length)]);
                }
            }

            return result;
        }
    
    }
}
