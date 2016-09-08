using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                    Name = "api1",
                    DisplayName = "API 1",
                    Description = "API 1 features and data",
                    Type = ScopeType.Resource,

                    ScopeSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    Claims = new List<ScopeClaim>
                    {
                        new ScopeClaim("role")
                    }
                },
        };
    }
}
