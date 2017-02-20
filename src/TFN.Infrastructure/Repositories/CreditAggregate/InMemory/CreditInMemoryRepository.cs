using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Models.Entities;

namespace TFN.Infrastructure.Repositories.CreditAggregate.InMemory
{
    public class CreditInMemoryRepository : ICreditRepository
    {
        public Task AddAsync(Credits entity)
        {
            InMemoryCredits.Credits.Add(entity);
            return Task.CompletedTask; 
        }

        public Task<Credits> GetAsync(Guid id)
        {
            return Task.FromResult(InMemoryCredits.Credits.SingleOrDefault(x => x.Id == id));
        }

        public Task UpdateAsync(Credits entity)
        {
            InMemoryCredits.Credits.RemoveAll(x => x.Id == entity.Id);
            InMemoryCredits.Credits.Add(entity);
            return Task.CompletedTask;
        }

        public Task<Credits> GetByUsername(string username)
        {
            return Task.FromResult(InMemoryCredits.Credits.SingleOrDefault(x => x.Username == username));
        }

        public Task<Credits> GetByUserId(Guid userId)
        {
            return Task.FromResult(InMemoryCredits.Credits.SingleOrDefault(x => x.UserId == userId));
        }

        public Task<IReadOnlyList<Credits>> GetHighestCredits(int offset, int limit)
        {
            IReadOnlyList<Credits> leaders =
                InMemoryCredits.Credits.OrderBy(x => x.TotalCredits).Skip(offset).Take(limit).ToList();
            
            return Task.FromResult(leaders);
        }
        public Task<IReadOnlyList<Credits>> SearchUsers(string searchToken, int offset, int limit)
        {
            IReadOnlyList<Credits> credits =
                InMemoryCredits.Credits.Where(x => x.Username.StartsWith(searchToken)).Skip(offset).Take(limit).ToList();
            return Task.FromResult(credits);
        }
    }
}