﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using musicList2.Database;
using musicList2.Filter;
using musicList2.Models;
using musicList2.Shared;
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
        public async Task<IActionResult> CreateList(
            [FromBody, Bind("Listidentifier", "Keyword")] AuthorizationModel listAuth)
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

            var masterKey = SecureRandom.GenerateMasterKey(32);
            list.MasterKeyHash = Hashing.CreatePasswordHash(masterKey);

            await db.Lists.AddAsync(list);
            await db.SaveChangesAsync();

            var outList = new ListCreated(list, masterKey);
            
            return Created("list", outList);
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

        [HttpDelete]
        [Authorize]
        [RateLimited(3, 3)]
        [SetCurrentList]
        [ServiceFilter(typeof(AuthorizeMasterKey))]
        public async Task<IActionResult> DeleteList(
            [FromBody, Bind("MasterKey")] ListMasterKeyModel model)
        {
            var list = db.Lists.Find(CurrentListGUID);
            if (list == null)
            {
                return NotFound(ErrorModel.NotFound());
            }

            db.Lists.Remove(list);
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("password")]
        [Authorize]
        [RateLimited(3, 3)]
        [SetCurrentList]
        [ServiceFilter(typeof(AuthorizeMasterKey))]
        public async Task<IActionResult> ChangePassword(
            [FromBody, Bind("MasterKey", "NewKeyword")] ListPasswordChangeModel model)
        {
            var list = db.Lists.Find(CurrentListGUID);
            if (list == null)
            {
                return NotFound(ErrorModel.NotFound());
            }

            if (model.NewKeyword == null || model.NewKeyword.Length < 1)
            {
                return BadRequest(ErrorModel.BadRequest());
            }

            list.KeywordHash = Hashing.CreatePasswordHash(model.NewKeyword);
            db.Lists.Update(list);
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("checkMasterKey")]
        [Authorize]
        [RateLimited(1, 5)]
        [SetCurrentList]
        [ServiceFilter(typeof(AuthorizeMasterKey))]
        public IActionResult CheckMasterKey(
            [FromBody, Bind("MasterKey")] ListMasterKeyModel model) =>
                Ok();
    }
}
