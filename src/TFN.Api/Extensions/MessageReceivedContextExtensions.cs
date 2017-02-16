using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Primitives;

namespace TFN.Api.Extensions
{
    public static class MessageReceivedContextExtensions
    {
        public static void GetTokenFromQueryString(this MessageReceivedContext context)
        {

            if (context.Request.QueryString.HasValue)
            {
                if (context.Request.Query.ContainsKey("access_token"))
                {
                    StringValues token = String.Empty;

                    var succeed = context.Request.Query.TryGetValue("access_token", out token);
                    if (succeed)
                    {
                        context.Token = token;

                    }
                }
            }
        }
    }
}