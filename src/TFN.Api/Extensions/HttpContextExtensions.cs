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

        public static string GetUsername(this HttpContext context)
        {
            var userName = context.User.Identity.Name;

            return userName;
        }

        public static string GetAbsoluteUri(this HttpContext context)
        {
            var request = context.Request;

            var host = request.Host.ToUriComponent();
            var scheme = request.Scheme;

            if (!(host.Contains("localhost") || host.Contains("127.0.0.1")))
            {
                scheme = "https";
            }

            return String.Concat(
                    scheme,
                    "://",
                    request.Host.ToUriComponent());
        }

        public static string GetAppUrl(this HttpContext context)
        {
            var http = "http://";
            if (context.Request.IsHttps)
            {
                http = "https://";
            }

            return $"{http}{context.Request.Host.Value}";
        }
    }
}
