using System;
using System.Threading.Tasks;
using Wimt.DomainDrivenArchitecture.Domain.Models;

namespace Wimt.DomainDrivenArchitecture.Domain.Repositories
{
    public interface IDeleteableRepository<TDomainEntity, TKey> : IRepository<TDomainEntity, TKey>
        where TDomainEntity : DomainEntity<TKey>, IAggregateRoot
    {
        Task DeleteAsync(Guid id);
    }
}