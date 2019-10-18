using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace musicList2.Models
{
    /// <summary>
    /// KeywordAccessLayer interface.
    /// </summary>
    public interface IKeywordAccessLayer
    {
        bool ValidateLogin(List list, string keyword);
    }
}
