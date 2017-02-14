using System;
using System.Linq;
using System.Threading.Tasks;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Models.Entities;

namespace TFN.Infrastructure.Repositories.TransientUserAggregate.InMemory
{
    public class TransientUserInMemoryRepository : ITransientUserRepository
    {
        public Task AddAsync(TransientUser entity)
        {
            InMemoryTransientUsers.TransientUsers.Add(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            InMemoryTransientUsers.TransientUsers.RemoveAll(x => x.Id == id);
            return Task.CompletedTask;
        }

        public Task<TransientUser> GetAsync(Guid id)
        {
            return Task.FromResult(InMemoryTransientUsers.TransientUsers.SingleOrDefault(x => x.Id == id));
        }

        public Task<TransientUser> GetByEmailAsync(string email)
        {
            return Task.FromResult(InMemoryTransientUsers.TransientUsers.SingleOrDefault(x => x.Email == email));
        }

        public Task<TransientUser> GetByEmailVerificationKeyAsync(string emailVerificationKey)
        {
            return Task.FromResult(InMemoryTransientUsers.TransientUsers.SingleOrDefault(x => x.EmailVerificationKey == emailVerificationKey));
        }

        public Task UpdateAsync(TransientUser entity)
        {
            DeleteAsync(entity.Id);
            AddAsync(entity);
            return Task.CompletedTask;
        }
    }
}