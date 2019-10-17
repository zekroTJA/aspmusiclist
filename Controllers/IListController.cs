using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace musicList2.Controllers
{
    interface IListController
    {
        Guid CurrentListGUID { get; set; }
    }
}
