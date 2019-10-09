using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using musicList2.Database;
using musicList2.Models;

namespace musicList2.Controllers
{
    [Route("api/list")]
    [Produces(MediaTypeNames.Application.Json)]
    public class ListController : Controller
    {
        private readonly SQLiteDbContext db = new SQLiteDbContext();

        [HttpGet("entries")]
        public IActionResult GetEntries()
        {
            List<ListEntry<string>> entries = db.Entries.ToList();
            return Ok(entries);
        }

        [HttpPost("entries")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CreateEntry([Bind("Content"), FromBody] ListEntryPostModel entry) {
            if (ModelState.IsValid)
                return BadRequest();

            if (entry.Content == null || entry.Content == "")
                return BadRequest(new { error = "'content' must be present" });

            var listEntry = new ListEntry<string>(entry.Content);

            db.Add(listEntry);
            await db.SaveChangesAsync();

            return CreatedAtAction("entry created", listEntry);
        }
    }
}
