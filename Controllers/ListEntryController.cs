using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using musicList2.Database;
using musicList2.Filter;
using musicList2.Models;

namespace musicList2.Controllers
{
    [Authorize]
    [SetCurrentList]
    [Route("api/list/entries")]
    [Produces(MediaTypeNames.Application.Json)]
    public class ListEntryController : Controller, IListController
    {
        private readonly AppDbContext db;

        public Guid CurrentListGUID { get; set; }

        public ListEntryController(AppDbContext _db)
        {
            db = _db;
        }

        [HttpGet]
        public IActionResult GetEntries()
        {
            List<ListEntry<string>> entries = db.Entries.Where(e => e.ListGUID == CurrentListGUID).ToList();

            return Ok(entries);
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CreateEntry([Bind("Content"), FromBody] ListEntryPostModel entry) {
            if (!ModelState.IsValid)
                return BadRequest(ErrorModel.BadRequest());

            if (entry.Content == null || entry.Content == "")
                return BadRequest(ErrorModel.BadRequest());

            var listEntry = new ListEntry<string>(entry.Content, CurrentListGUID);

            db.Entries.Add(listEntry);
            await db.SaveChangesAsync();

            return CreatedAtAction("entry created", listEntry);
        }

        [HttpDelete("{entryId}")]
        public async Task<IActionResult> DeleteEntry(Guid entryId)
        {
            if (entryId == null)
                return BadRequest(ErrorModel.BadRequest());

            var entry = db.Entries
                .FirstOrDefault(e => e.ListGUID == CurrentListGUID && e.GUID == entryId);

            if (entry == null)
                return NotFound(ErrorModel.NotFound());

            db.Entries.Remove(entry);
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("flush")]
        public async Task<IActionResult> Flush()
        {
            var entries = db.Entries
                .Where(e => e.ListGUID == CurrentListGUID)
                .ToArray();

            db.Entries.RemoveRange();
            await db.SaveChangesAsync();

            return Ok();
        }
    }
}
