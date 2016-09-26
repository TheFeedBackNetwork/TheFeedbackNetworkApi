using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.Models;
using TFN.Domain.Interfaces.Repositories;

namespace TFN.Infrastructure.Repositories.ClientAggregate.InMemory
{
    public class ClientInMemoryRepository : IClientRepository
    {
        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<string>> GetAllAllowedCorsOriginsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
