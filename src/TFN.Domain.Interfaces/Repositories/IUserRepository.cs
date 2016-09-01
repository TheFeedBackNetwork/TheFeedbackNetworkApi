using System;
using System.Threading.Tasks;
using TFN.Domain.Models.Entities;
using TFN.DomainDrivenArchitecture.Domain.Repositories;

namespace TFN.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User, Guid>, IAddableRepository<User, Guid>, IDeleteableRepository<User, Guid>, IUpdateableRepository<User,Guid>
    {
        Task<User> FindAsync(string username);
    }
}
