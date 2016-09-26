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
        public Task AddAsync(User entity)
        {
            //this one wont be needed for a bit
            throw new NotImplementedException();
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

        public async Task AddBulkAsync(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<User> AutoProvisionUserAsync(string provider, string userId, List<Claim> claims)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> FindByExternalProviderAsync(string provider, string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetAsync(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetAsync(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ValidateCredentialsAsync(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
