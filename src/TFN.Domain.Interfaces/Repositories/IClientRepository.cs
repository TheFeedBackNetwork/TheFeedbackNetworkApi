using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.Stores;

namespace TFN.Domain.Interfaces.Repositories
{
    public interface IClientRepository : IClientStore
    {
        Task<IEnumerable<string>> GetAllAllowedCorsOriginsAsync();
    }
}
