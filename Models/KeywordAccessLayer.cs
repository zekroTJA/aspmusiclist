using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DevOne.Security.Cryptography.BCrypt;
using musicList2.Exceptions;
using musicList2.Database;
using musicList2.Shared;

namespace musicList2.Models
{
    public class KeywordAccessLayer : IKeywordAccessLayer
    {
        private readonly AppDbContext db;

        public KeywordAccessLayer(AppDbContext _db)
        {
            db = _db;
        }

        public bool ValidateLogin(List list, string keyword) => 
            Hashing.CompareStringToHash(keyword, list.KeywordHash);
    }
}
