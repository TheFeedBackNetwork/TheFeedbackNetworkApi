using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using TFN.Domain.Interfaces.Repositories;

namespace TFN.Infrastructure.Repositories.ScopeAggregate.InMemory
{
    public class ScopeInMemoryRepository : IScopeRepository
    {
        public Task<IEnumerable<Scope>> FindScopesAsync(IEnumerable<string> scopeNames)
        {
            if (scopeNames == null)
            {
                throw new ArgumentNullException(nameof(scopeNames));
            }

            var scopes = from s in InMemoryScopes.Scopes
                         where scopeNames.ToList().Contains(s.Name)
                         select s;

            return Task.FromResult<IEnumerable<Scope>>(scopes.ToList());

        }

        public Task<IEnumerable<Scope>> GetScopesAsync(bool publicOnly = true)
        {
            if (publicOnly)
            {
                return Task.FromResult(InMemoryScopes.Scopes.Where(s => s.ShowInDiscoveryDocument));
            }

            return Task.FromResult(InMemoryScopes.Scopes);
        }
    }
}
