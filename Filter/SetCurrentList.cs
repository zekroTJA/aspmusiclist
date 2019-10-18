using Microsoft.AspNetCore.Mvc.Filters;
using musicList2.Controllers;
using musicList2.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace musicList2.Filter
{
    /// <summary>
    /// Attribute Filter for setting the current list GUID
    /// got from the User Claims in Controlelrs implementing
    /// IListController.
    /// </summary>
    public class SetCurrentList : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var listGUID = context.HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ExtendedClaimTypes.ListGUID)?.Value;

            var controller = (IListController) context.Controller;
            controller.CurrentListGUID = Guid.Parse(listGUID);
        }
    }
}
