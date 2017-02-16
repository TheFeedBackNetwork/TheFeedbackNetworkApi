using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR.Hubs;

namespace TFN.Api.Extensions
{
    public static class HubCallerContextExtensions
    {
        public static Guid? GetUserId(this HubCallerContext context)
        {
            var claims = new ClaimsPrincipal(context.User);

            var userId = claims.Claims.FirstOrDefault(x => x.Type == "sub");

            if (userId?.Value != null)
            {
                return Guid.Parse(userId.Value);
            }
            else
            {
                return null;
            }
        }

        public static string GetUsername(this HubCallerContext context)
        {
            var userName = context.User.Identity.Name;

            return userName;
        }
    }
}