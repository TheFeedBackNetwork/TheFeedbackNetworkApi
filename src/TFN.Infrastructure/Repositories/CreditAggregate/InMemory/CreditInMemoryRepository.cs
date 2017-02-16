using System;
using System.Linq;
using System.Threading.Tasks;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Models.Entities;

namespace TFN.Infrastructure.Repositories.CreditAggregate.InMemory
{
    public class CreditInMemoryRepository : ICreditRepository
    {
        public Task AddAsync(Credit entity)
        {
            InMemoryCredits.Credits.Add(entity);
            return Task.CompletedTask;
        }

        public Task<Credit> GetAsync(Guid id)
        {
            return Task.FromResult(InMemoryCredits.Credits.SingleOrDefault(x => x.Id == id));
        }

        public Task UpdateAsync(Credit entity)
        {
            InMemoryCredits.Credits.RemoveAll(x => x.Id == entity.Id);
            InMemoryCredits.Credits.Add(entity);
            return Task.CompletedTask;
        }

        public Task<Credit> GetByUsername(string username)
        {
            return Task.FromResult(InMemoryCredits.Credits.SingleOrDefault(x => x.Username == username));
        }
    }
}