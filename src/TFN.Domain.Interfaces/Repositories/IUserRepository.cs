using System;
using System.Threading.Tasks;
using TFN.Domain.Models.Entities;
using TFN.DomainDrivenArchitecture.Domain.Repositories;

namespace TFN.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IAddableRepository<User, Guid>, IDeleteableRepository<User, Guid>, IUpdateableRepository<User,Guid>
    {
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByChangePasswordKey(string changePasswordKey);
        Task<User> GetAsync(string usernameOrEmail,string password);
        Task AddAsync(User entity, string password);
        Task UpdateChangePasswordKeyAsync(User user, string changePasswordKey);
        Task UpdateUserPasswordAsync(User user, string password);
        Task<bool> ChangePasswordKeyExistsAsync(string changePasswordKey);
    }
}
