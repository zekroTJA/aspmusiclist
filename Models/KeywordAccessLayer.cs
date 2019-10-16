using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DevOne.Security.Cryptography.BCrypt;
using musicList2.Exceptions;

namespace musicList2.Models
{
    public class KeywordAccessLayer : IKeywordAccessLayer
    {
        private readonly string keywordHash;

        public KeywordAccessLayer(IConfiguration configuration)
        {
            keywordHash = configuration["Authorization:KeywordHash"];
            if (keywordHash == null || keywordHash == "")
            {
                throw new NotConfiguredException("Authorization:KeywordHash");
            }
        }

        public bool ValidateLogin(string kw)
        {
            var kwSplit = kw.Split(" ");
            if (kwSplit.Length <= 1)
                return false;

            if (kwSplit[0].ToLower() != "basic")
                return false;

            try
            {
                return BCryptHelper.CheckPassword(kwSplit[1], keywordHash);
            } catch (Exception)
            {
                return false;
            }
        }
    }
}
