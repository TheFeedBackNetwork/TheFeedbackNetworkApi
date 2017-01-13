using System;
using System.Threading.Tasks;
using TFN.Domain.Models.Entities;
using TFN.DomainDrivenArchitecture.Domain.Repositories;

namespace TFN.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IAddableRepository<User, Guid>, IDeleteableRepository<User, Guid>, IUpdateableRepository<User,Guid>
    {
        Task<User> GetAsync(string username);
        Task<User> GetAsync(string username,string password);
        Task AddAsync(User entity, string password);
    }
}
