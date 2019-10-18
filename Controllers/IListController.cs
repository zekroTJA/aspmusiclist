using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace musicList2.Controllers
{
    /// <summary>
    /// Interface for a Controller Containing a CurrentListGUID
    /// which can be then injected by the SetCurrentList 
    /// Attribute Filter.
    /// </summary>
    interface IListController
    {
        Guid CurrentListGUID { get; set; }
    }
}
