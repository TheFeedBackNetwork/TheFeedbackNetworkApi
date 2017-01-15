using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using TFN.Domain.Interfaces.Repositories;

namespace TFN.Infrastructure.Repositories.ClientAggregate.InMemory
{
    #pragma warning disable 1998
    //TODO Remove when we async
    public class ClientInMemoryRepository : IClientRepository
    {
        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            return InMemoryClients.Clients.SingleOrDefault(x => x.ClientId == clientId);
        }

        public async Task<IEnumerable<string>> GetAllAllowedCorsOriginsAsync()
        {
            var origins = InMemoryClients.Clients.SelectMany(x => x.AllowedCorsOrigins);
            return origins;
        }
    }
}
