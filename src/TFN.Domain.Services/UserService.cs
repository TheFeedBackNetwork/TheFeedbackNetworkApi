using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Interfaces.Services;
using TFN.Domain.Models.Entities;

namespace TFN.Domain.Services
{
    public class UserService : IUserService
    {
        public IUserRepository UserRepository { get; private set; }
        public UserService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }
        public async Task AddAsync(User entity)
        {
            await UserRepository.AddAsync(entity);
        }

        public async Task AddAsync(User entity, string password)
        {

            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(entity)}");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException($"{nameof(password)}");
            }

            await UserRepository.AddAsync(entity, password);
        }

        public async Task DeleteAsync(Guid id)
        {
            await UserRepository.DeleteAsync(id);
        }

        public async Task<User> GetAsync(Guid id)
        {
            var user = await UserRepository.GetAsync(id);

            return user;
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            var user = await UserRepository.GetByUsernameAsync(username);

            return user;
        }

        public async Task<User> GetAsync(string username, string password)
        {
            var user = await UserRepository.GetAsync(username, password);
            return user;
        }

        public async Task UpdateAsync(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(entity)}");
            }

            await UserRepository.UpdateAsync(entity);
        }

        public async Task<bool> ValidateCredentialsAsync(string username, string password)
        {
            var user = await UserRepository.GetAsync(username, password);

            return user != null;
        }

        public async Task<bool> ExistsByEmail(string email)
        {
            var user = await UserRepository.GetByEmailAsync(email);

            return user != null;
        }

        public async Task<bool> ExistsByUsername(string username)
        {
            var user = await UserRepository.GetByUsernameAsync(username);

            return user != null;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var user = await UserRepository.GetByEmailAsync(email);

            return user;
        }
        #pragma warning disable 1998
        //TODO Remove when we async
        public async Task<User> FindByExternalProviderAsync(string provider, string userId)
        {
            throw new NotImplementedException();
        }
        #pragma warning disable 1998
        //TODO Remove when we async
        public async Task<User> AutoProvisionUserAsync(string provider, string userId, List<Claim> claims)
        {
            throw new NotImplementedException();
        }
    }
}
