using System;
using System.Security.Cryptography;

namespace musicList2.Shared
{
    public class SecureRandom
    {
        public static string GenerateMasterKey(int len)
        {
            var bytes = new byte[len];
            RandomNumberGenerator.Fill(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
