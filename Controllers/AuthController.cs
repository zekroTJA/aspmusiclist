using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using musicList2.Database;
using musicList2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;

namespace musicList2.Controllers
{
    [Route("api/auth")]
    [Produces(MediaTypeNames.Application.Json)]
    public class AuthController : Controller
    {
        private readonly IKeywordAccessLayer keywordAccess;

        public AuthController(IKeywordAccessLayer _keywordAccess)
        {
            keywordAccess = _keywordAccess;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login()
        {
            var authHeaderValue = HttpContext.Request.Headers["Authorization"];

            if (!keywordAccess.ValidateLogin(authHeaderValue))
            {
                return Unauthorized();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "anonymous"),
            };

            var identity = new ClaimsIdentity(claims, "login");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(principal);
            return Ok();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }
    }
}
