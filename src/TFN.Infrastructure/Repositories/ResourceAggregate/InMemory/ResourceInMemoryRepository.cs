using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using TFN.Domain.Interfaces.Repositories;

namespace TFN.Infrastructure.Repositories.ResourceAggregate.InMemory
{
    public class ResourceInMemoryRepository : IResourceRepository
    {
        public Task<ApiResource> FindApiResourceAsync(string name)
        {
            var resource = InMemoryResources.ApiResources.FirstOrDefault(x => x.Name == name);

            return Task.FromResult(resource);
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            if (scopeNames == null)
                throw new ArgumentNullException(nameof(scopeNames));

            var resources = InMemoryResources.ApiResources.Where(x => scopeNames.Contains(x.Name));

            return Task.FromResult(resources);
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            if (scopeNames == null)
                throw new ArgumentNullException(nameof(scopeNames));

            var identity = InMemoryResources.IdentityResources.Where(x => scopeNames.Contains(x.Name));

            return Task.FromResult(identity);
        }

        public Task<Resources> GetAllResources()
        {
            var result = new Resources(InMemoryResources.IdentityResources,InMemoryResources.ApiResources);
            return Task.FromResult(result);
        }
    }
}