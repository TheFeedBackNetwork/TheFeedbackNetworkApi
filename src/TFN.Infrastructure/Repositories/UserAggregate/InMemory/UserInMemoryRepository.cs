using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Interfaces.Services;
using TFN.Domain.Models.Entities;

namespace TFN.Infrastructure.Repositories.UserAggregate.InMemory
{
    public class UserInMemoryRepository : IUserRepository
    {
        public static Dictionary<string, string> ChangePasswordKeys = new Dictionary<string, string>();

        public static Dictionary<string, string> Passwords = new Dictionary<string, string>
        {
            {
                "f42c8b85-c058-47cb-b504-57f750294469",
                "2710.AMjdCBvWAjoqwP4U9uhyxGSfShdrqfS746Qpls9WDOA5pdFv1uQk4w8Pbo3Dx6jQtA=="
            },
            {
                "3f9969b7-c267-4fc5-bedf-b05d211ba1d6",
                "2710.AMjdCBvWAjoqwP4U9uhyxGSfShdrqfS746Qpls9WDOA5pdFv1uQk4w8Pbo3Dx6jQtA=="
            },
            {
                "b984088b-bbab-4e3e-9a40-c07238475cb7",
                "2710.AMjdCBvWAjoqwP4U9uhyxGSfShdrqfS746Qpls9WDOA5pdFv1uQk4w8Pbo3Dx6jQtA=="
            },

        };
        public IPasswordService PasswordService { get; private set; }
        public UserInMemoryRepository(IPasswordService passwordService)
        {
            PasswordService = passwordService;
       
        }
        public Task AddAsync(User entity)
        {
            InMemoryUsers.Users.Add(entity);
            return Task.FromResult(0);
        }

        public Task AddAsync(User entity, string password)
        {
            var hashedPassword = PasswordService.HashPassword(password);
            Passwords.Add(entity.Id.ToString(),hashedPassword);
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

        public async Task<User> GetByUsernameAsync(string username, string password)
        {

            var user = await GetByUsernameAsync(username);

            if (user != null)
            {
                var hashedPass = Passwords[user.Id.ToString()];

                var verified = PasswordService.VerifyHashedPassword(hashedPass, password);
                if (verified)
                {
                    return user;
                }
            }

            return null;
        }

        public async Task<User> GetByEmailAsync(string username, string password)
        {

            var user = await GetByEmailAsync(username);

            if (user != null)
            {
                var hashedPass = Passwords[user.Id.ToString()];

                var verified = PasswordService.VerifyHashedPassword(hashedPass, password);
                if (verified)
                {
                    return user;
                }
            }

            return null;
        }

        public async Task UpdateAsync(User entity)
        {
            await DeleteAsync(entity.Id);
            await AddAsync(entity);
        }

        public Task<User> GetByEmailAsync(string email)
        {
            return Task.FromResult(InMemoryUsers.Users.SingleOrDefault(x => x.Email == email));
        }

        public Task UpdateChangePasswordKeyAsync(User user, string changePasswordKey)
        {
            //ChangePasswordKeys.Add(changePasswordKey,user.Id.ToString());
            ChangePasswordKeys[changePasswordKey] = user.Id.ToString();
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
            var hashedPass = PasswordService.HashPassword(password);

            Passwords[user.Id.ToString()] = hashedPass;

            //clear key
            ChangePasswordKeys.Remove(user.Id.ToString());

            return Task.CompletedTask;
        }
    }
}
