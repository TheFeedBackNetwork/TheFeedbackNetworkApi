using System;
using System.Linq;
using System.Threading.Tasks;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Models.Entities;

namespace TFN.Infrastructure.Repositories.UserAggregate.InMemory
{
    public class UserInMemoryRepository : IUserRepository
    {
        public Task AddAsync(User entity)
        {
            InMemoryUsers.Users.Add(entity);
            return Task.FromResult(0);
        }

        public Task AddAsync(User entity, string password)
        {
            InMemoryUsers.Users.Add(entity);
            return Task.FromResult(0);
        }

        public Task DeleteAsync(Guid id)
        {
            InMemoryUsers.Users.RemoveAll(x => x.Id == id);
            return Task.FromResult(0);
        }

        public Task<User> GetAsync(Guid id)
        {
            return Task.FromResult(InMemoryUsers.Users.SingleOrDefault(x => x.Id == id));
        }

        public Task<User> GetByUsernameAsync(string username)
        {
            return Task.FromResult(InMemoryUsers.Users.SingleOrDefault(x => x.Username == username));
        }

        public Task<User> GetAsync(string usernameOrEmail, string password)
        {
            var user = GetByUsernameAsync(usernameOrEmail);

            if (user == null)
            {
                user = GetByEmailAsync(usernameOrEmail);
            }

            return user;
        }

        public Task UpdateAsync(User entity)
        {
            DeleteAsync(entity.Id);
            AddAsync(entity);
            return Task.FromResult(0);
        }

        public Task<User> GetByEmailAsync(string email)
        {
            return Task.FromResult(InMemoryUsers.Users.SingleOrDefault(x => x.Email == email));
        }
    }
}
