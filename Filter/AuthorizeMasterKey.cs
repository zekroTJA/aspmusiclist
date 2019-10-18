using Microsoft.AspNetCore.Mvc.Filters;
using musicList2.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            //var listGUID context.Controller;
            Console.WriteLine($"DB IS NULLL? {db == null}");
        }
    }
}
