using System;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Gybs;

namespace GamersHub.Api.Extensions
{
    public static class Extensions
    {
        public static Guid GetUserId(this HttpContext httpContext)
        {
            return httpContext.User == null
                ? Guid.Empty
                : Guid.Parse(httpContext.User.Claims.Single(x => x.Type == "id").Value);
        }

        public static bool HasFailed(this IResult result)
        {
            return !result.HasSucceeded;
        }
    }
}
