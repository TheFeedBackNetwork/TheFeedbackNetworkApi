using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace TFN.Mvc.Extensions
{
    public static class AuthorizationOptionsExtensions
    {
        public static AuthorizationOptions AddScopePolicy(this AuthorizationOptions options, string name)
        {
            options.AddPolicy(name, new AuthorizationPolicy(new IAuthorizationRequirement[]
                {
                    new ClaimsAuthorizationRequirement("Scope", new []{name}),
                    new DenyAnonymousAuthorizationRequirement(),
                }, new[] { "Bearer" }));

            return options;
        }
    }
}
