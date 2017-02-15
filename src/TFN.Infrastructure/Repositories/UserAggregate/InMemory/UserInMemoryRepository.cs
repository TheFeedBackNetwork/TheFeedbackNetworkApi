using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Models.Entities;

namespace TFN.Infrastructure.Repositories.UserAggregate.InMemory
{
    public class UserInMemoryRepository : IUserRepository
    {
        public static Dictionary<string, string> ChangePasswordKeys = new Dictionary<string, string>();
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

        public Task UpdateChangePasswordKeyAsync(User user, string changePasswordKey)
        {
            ChangePasswordKeys.Add(changePasswordKey,user.Id.ToString());

            return Task.CompletedTask;
        }

        public Task<bool> ChangePasswordKeyExistsAsync(string changePasswordKey)
        {
            return Task.FromResult(ChangePasswordKeys.ContainsKey(changePasswordKey));
        }

        public Task<User> GetByChangePasswordKey(string changePasswordKey)
        {
            var id = ChangePasswordKeys[changePasswordKey];
            if (id != null)
            {
                return Task.FromResult(InMemoryUsers.Users.SingleOrDefault(x => x.Id.ToString() == id));
            }

            return Task.FromResult<User>(null);
        }

        public Task UpdateUserPasswordAsync(User user, string password)
        {
            throw new NotImplementedException();
        }
    }
}
