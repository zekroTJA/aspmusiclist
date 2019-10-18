using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using musicList2.Models;
using musicList2.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace musicList2.Filter
{
    /// <summary>
    /// Attribute Filter for rate limiting requests on
    /// request handlers on a per-connection and
    /// per-route base.
    /// 
    /// If the rate limit is exceed by the client, the
    /// request will be canceled with an 429 Too Many 
    /// Requests response. Also, the headers
    /// "X-Ratelimit-Limit", "X-Ratelimit-Remaining" and
    /// "X-Ratelimit-Reset" are being added to identify
    /// the state of the rate limiter.
    /// </summary>
    public class RateLimited : ActionFilterAttribute
    {
        private readonly Dictionary<IPAddress, RateLimiter> limiters = new Dictionary<IPAddress, RateLimiter>();
        private readonly TimeSpan limit;
        private readonly int burst;

        public RateLimited(int limitSeconds = 1, int _burst = 5)
        {
            limit = TimeSpan.FromSeconds(limitSeconds);
            burst = _burst;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var remoteAddress = context.HttpContext.Connection.RemoteIpAddress;

            if (remoteAddress == null)
            {
                context.Result = new BadRequestObjectResult(ErrorModel.BadRequest());
                return;
            }

            RateLimiter limiter;

            if (limiters.ContainsKey(remoteAddress))
            {
                limiter = limiters[remoteAddress];
            } else
            {
                limiter = new RateLimiter(limit, burst);
                limiters.Add(remoteAddress, limiter);

            }

            var reservation = limiter.Reserve();

            AddReservationHeader(context, reservation);

            if (!reservation.Success)
            {
                var result = new ObjectResult(ErrorModel.RateLimited());
                result.StatusCode = 429;
                context.Result = result;
                return;
            }
        }

        private void AddReservationHeader(ActionContext context, Reservation reservation)
        {
            var headers = context.HttpContext.Response.Headers;

            headers.Add("X-Ratelimit-Limit", reservation.Burst.ToString());
            headers.Add("X-Ratelimit-Remaining", reservation.Remaining.ToString());
            headers.Add("X-Ratelimit-Reset", reservation.Reset.ToString("o"));
        }
    }
}
