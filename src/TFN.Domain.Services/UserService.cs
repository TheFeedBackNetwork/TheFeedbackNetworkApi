using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFN.Domain.Interfaces.Services;
using TFN.Domain.Models.Entities;

namespace TFN.Domain.Services
{
    public class UserService : IUserService
    {
        public Task AddAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task AddBulkAsync(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetAsync(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
