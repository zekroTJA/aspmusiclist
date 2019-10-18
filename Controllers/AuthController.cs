using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using musicList2.Database;
using musicList2.Extensions;
using musicList2.Filter;
using musicList2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;

namespace musicList2.Controllers
{
    /// <summary>
    /// Controller handling login and logout for
    /// lists.
    /// </summary>
    [Route("api/auth")]
    [Produces(MediaTypeNames.Application.Json)]
    public class AuthController : Controller
    {
        private readonly IKeywordAccessLayer keywordAccess;
        private readonly AppDbContext db;

        public AuthController(IKeywordAccessLayer _keywordAccess, AppDbContext _db)
        {
            keywordAccess = _keywordAccess;
            db = _db;
        }

        [HttpPost("login")]
        [RateLimited(10, 3)]
        public async Task<IActionResult> Login([FromBody, Bind("ListIdentifier", "Keyword")] AuthorizationModel auth)
        {
            if (!auth.Validate())
            {
                return BadRequest(ErrorModel.BadRequest());
            }

            auth.LowerIdentifier();

            var list = db.Lists.FirstOrDefault(l => l.Identifier == auth.ListIdentifier);
            if (list == null)
            {
                return Unauthorized();
            }

            if (!keywordAccess.ValidateLogin(list, auth.Keyword))
            {
                return Unauthorized();
            }

            var claims = new List<Claim>
            {
                new Claim(ExtendedClaimTypes.ListIdentifier, list.Identifier),
                new Claim(ExtendedClaimTypes.ListGUID, list.GUID.ToString()),
            };

            var identity = new ClaimsIdentity(claims, "login");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(principal);
            return Ok();
        }

        [HttpPost("logout")]
        [RateLimited]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }
    }
}
