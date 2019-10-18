using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace musicList2.Extensions
{
    /// <summary>
    /// Additionally used Authorization Cookie Claim Types.
    /// </summary>
    public static class ExtendedClaimTypes
    {
        public const string ListIdentifier = "MusicList.List.Identifier";
        public const string ListGUID = "MusicList.List.GUID";
    }
}
