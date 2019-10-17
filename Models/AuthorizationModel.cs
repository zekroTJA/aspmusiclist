using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace musicList2.Models
{
    public class AuthorizationModel
    {
        public string ListIdentifier;
        public string Keyword;

        public bool Validate() =>
            ListIdentifier != null && ListIdentifier.Length > 0 &&
            Keyword != null && Keyword.Length > 0;

        public void LowerIdentifier()
        {
            ListIdentifier = ListIdentifier.ToLower();
        }
    }
}
