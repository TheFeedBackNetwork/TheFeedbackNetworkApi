using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace TFN.Domain.Services
{
    public class UserIdProvider : IUserIdProvider
    {
        public string GetUserId(HttpRequest request)
        {
            var userId = request.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "sub");

            if (userId?.Value != null)
            {
                return Guid.Parse(userId.Value).ToString();
            }

            return null;
        }
    }
}