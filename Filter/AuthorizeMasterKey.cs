using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using musicList2.Controllers;
using musicList2.Database;
using musicList2.Models;
using musicList2.Shared;
using System;
using System.Linq;

namespace musicList2.Filter
{
    public class AuthorizeMasterKey : ActionFilterAttribute
    {
        private readonly AppDbContext db;

        public AuthorizeMasterKey(AppDbContext _db)
        {
            db = _db;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var listGUID = ((IListController) context.Controller).CurrentListGUID;
            var currList = db.Lists.Find(listGUID);

            if (currList == null)
            {
                context.Result = new NotFoundObjectResult(ErrorModel.NotFound());
                return;
            }

            var masterKeyModel = context.ActionArguments.Values.FirstOrDefault(v => v is IMasterKey) as IMasterKey;

            if (masterKeyModel == null)
            {
                context.Result = new BadRequestObjectResult(ErrorModel.BadRequest());
                return;
            }

            if (!Hashing.CompareStringToHash(masterKeyModel.MasterKey, currList.MasterKeyHash))
            {
                var res = new ObjectResult(ErrorModel.Unauthorized());
                res.StatusCode = 401;
                context.Result = res;
                return;
            };
        }
    }
}
