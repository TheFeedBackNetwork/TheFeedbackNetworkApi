using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;

namespace TFN.Mvc.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseRequireHttps(this IApplicationBuilder builder, bool isDeveloper = false)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.Use(
                async (context, next) =>
                {
                    if (context.Request.IsHttps || isDeveloper)
                    {
                        await next();
                        return;
                    }

                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync("Only secure HTTPS connections permitted.");
                });
        }
    }
}