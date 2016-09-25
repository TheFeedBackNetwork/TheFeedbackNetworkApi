using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace TFN.Api.Extensions
{
    public static class HttpContextExtensions
    {
        public static Guid? GetUserId(this HttpContext context)
        {
            var userId = context.User.Claims.FirstOrDefault(x => x.Type == "sub");

            if (userId?.Value != null)
            {
                return Guid.Parse(userId.Value);
            }
            else
            {
                return null;
            }
        }
    }
}
