using DevOne.Security.Cryptography.BCrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace musicList2.Shared
{
    public class Hashing
    {
        public static string CreatePasswordHash(string password)
        {
            var salt = BCryptHelper.GenerateSalt();
            return BCryptHelper.HashPassword(password, salt);
        }

        public static bool CompareStringToHash(string pw, string hash)
        {
            try
            {
                return BCryptHelper.CheckPassword(pw, hash);
            } catch (Exception)
            {
                return false;
            }
        }
    }
}
