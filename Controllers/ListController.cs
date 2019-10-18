using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using musicList2.Database;
using musicList2.Filter;
using musicList2.Models;
using System;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace musicList2.Controllers
{
    /// <summary>
    /// Controller handling requests about actions
    /// on and with List objects.
    /// </summary>
    [Route("api/list")]
    [Produces(MediaTypeNames.Application.Json)]
    public class ListController : Controller, IListController
    {
        private readonly AppDbContext db;
        public Guid CurrentListGUID { get; set; }

        public ListController(AppDbContext _db)
        {
            db = _db;
        }


        [HttpPost]
        [RateLimited(60, 3)]
        public async Task<IActionResult> CreateList([FromBody, Bind("Listidentifier", "Keyword")] AuthorizationModel listAuth)
        {
            if (!listAuth.Validate())
            {
                return BadRequest(ErrorModel.BadRequest());
            }

            listAuth.LowerIdentifier();

            if (db.Lists.FirstOrDefault(l => l.Identifier == listAuth.ListIdentifier) != null)
            {
                return BadRequest(ErrorModel.AlreadyExists());
            }

            var list = new List(listAuth.ListIdentifier, listAuth.Keyword);

            await db.Lists.AddAsync(list);
            await db.SaveChangesAsync();

            list.KeywordHash = "";
            return Created("list", list);
        }

        [HttpGet]
        [Authorize]
        [RateLimited]
        [SetCurrentList]
        public IActionResult GetList()
        {
            var list = db.Lists.Find(CurrentListGUID);
            if (list == null)
            {
                return NotFound(ErrorModel.NotFound());
            }

            return Ok(list);
        }
    }
}
