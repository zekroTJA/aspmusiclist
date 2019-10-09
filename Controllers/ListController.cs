using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using musicList2.Database;
using musicList2.Models;

namespace musicList2.Controllers
{
    [Route("api/list")]
    public class ListController : Controller
    {
        private readonly SQLiteDbContext db = new SQLiteDbContext();

        [HttpPost("entries")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CreateEntry([Bind("Content"), FromBody] ListEntryPostModel entry) {
            Console.WriteLine(entry.Content);
            Console.WriteLine(ModelState.IsValid);
            if (entry.Content == null || entry.Content == "")
                return BadRequest("'content' must be present");

            var listEntry = new ListEntry<string>(entry.Content);

            db.Add(listEntry);
            await db.SaveChangesAsync();

            return CreatedAtAction("entry created", listEntry);
        }

        public class TestOb {
            public string data;
        }
    }
}
