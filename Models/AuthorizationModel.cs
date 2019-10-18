using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace musicList2.Models
{
    /// <summary>
    /// Model for Authorization and List
    /// creation containing the list identifier,
    /// which must be always lowercase, and the
    /// plain text keyword to access the list.
    /// </summary>
    public class AuthorizationModel
    {
        public string ListIdentifier;
        public string Keyword;

        /// <summary>
        /// Returns true if ListIdentifier and Keyword
        /// are not null and have a length larger than 0.
        /// </summary>
        /// <returns></returns>
        public bool Validate() =>
            ListIdentifier != null && ListIdentifier.Length > 0 &&
            Keyword != null && Keyword.Length > 0;

        /// <summary>
        /// Lowercases the set ListIdentifier.
        /// </summary>
        public void LowerIdentifier()
        {
            ListIdentifier = ListIdentifier.ToLower();
        }
    }
}
