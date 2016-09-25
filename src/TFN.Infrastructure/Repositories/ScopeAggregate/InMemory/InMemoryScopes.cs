using System.Collections.Generic;
using IdentityModel;
using IdentityServer4.Models;

namespace TFN.Infrastructure.Repositories.ScopeAggregate.InMemory
{
    public class InMemoryScopes
    {
        public static IEnumerable<Scope> Scopes = new List<Scope>
        {
            StandardScopes.OpenId,
            StandardScopes.ProfileAlwaysInclude,
            StandardScopes.RolesAlwaysInclude,
            new Scope
            {
                Name = "biography",
                Description = "Non standard identity scope for holding biography information",
                Type = ScopeType.Identity
            },
            new Scope
            {
                Name = "profile_picture_url",
                Description = "Non standard identity scope for holding profile picture url",
                Type = ScopeType.Identity
            },
            new Scope
            {
                Name = "posts.read",
                DisplayName = "Posts Read",
                Description = "Scope for retreiving the posts resource",
                Type = ScopeType.Resource,
                Claims = new List<ScopeClaim>
                {
                    new ScopeClaim(JwtClaimTypes.Email, true),
                    new ScopeClaim(JwtClaimTypes.PreferredUserName, true),
                    new ScopeClaim(JwtClaimTypes.Picture, true),
                    new ScopeClaim(JwtClaimTypes.GivenName, true),
                    new ScopeClaim(JwtClaimTypes.FamilyName, true),
                    new ScopeClaim("biography", true),
                    new ScopeClaim("profile_picture_url", true),
                }
            },
            new Scope
            {
                Name = "posts.write",
                DisplayName = "Posts Write",
                Description = "Scope for writing to the posts resource",
                Type = ScopeType.Resource,
                Claims = new List<ScopeClaim>
                {
                    new ScopeClaim(JwtClaimTypes.Email, true),
                    new ScopeClaim(JwtClaimTypes.PreferredUserName, true),
                    new ScopeClaim(JwtClaimTypes.Picture, true),
                    new ScopeClaim(JwtClaimTypes.GivenName, true),
                    new ScopeClaim(JwtClaimTypes.FamilyName, true),
                    new ScopeClaim("biography", true),
                    new ScopeClaim("profile_picture_url", true),
                }
            },
            new Scope
            {
                Name = "posts.edit",
                DisplayName = "Posts Edit",
                Description = "Scope for editing the posts resource",
                Type = ScopeType.Resource,
                Claims = new List<ScopeClaim>
                {
                    new ScopeClaim(JwtClaimTypes.Email, true),
                    new ScopeClaim(JwtClaimTypes.PreferredUserName, true),
                    new ScopeClaim(JwtClaimTypes.Picture, true),
                    new ScopeClaim(JwtClaimTypes.GivenName, true),
                    new ScopeClaim(JwtClaimTypes.FamilyName, true),
                    new ScopeClaim("biography", true),
                    new ScopeClaim("profile_picture_url", true),
                }
            },
            new Scope
            {
                Name = "posts.delete",
                DisplayName = "Posts Edit",
                Description = "Scope for deleting the posts resource",
                Type = ScopeType.Resource,
                Claims = new List<ScopeClaim>
                {
                    new ScopeClaim(JwtClaimTypes.Email, true),
                    new ScopeClaim(JwtClaimTypes.PreferredUserName, true),
                    new ScopeClaim(JwtClaimTypes.Picture, true),
                    new ScopeClaim(JwtClaimTypes.GivenName, true),
                    new ScopeClaim(JwtClaimTypes.FamilyName, true),
                    new ScopeClaim("biography", true),
                    new ScopeClaim("profile_picture_url", true),
                }
            }
        };
    }
}
