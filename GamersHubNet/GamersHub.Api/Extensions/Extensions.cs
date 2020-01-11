using Microsoft.AspNetCore.Http;
using System.Linq;

namespace GamersHub.Api.Extensions
{
    public static class Extensions
    {
        public static string GetUserId(this HttpContext httpContext)
        {
            return httpContext.User == null
                ? string.Empty
                : httpContext.User.Claims.Single(x => x.Type == "id").Value;
        }
    }
}
