using System;
using System.Threading.Tasks;
using TFN.DomainDrivenArchitecture.Domain.Models;

namespace TFN.DomainDrivenArchitecture.Domain.Repositories
{
    public interface IDeleteableRepository<TDomainEntity, TKey> : IRepository<TDomainEntity, TKey>
        where TDomainEntity : DomainEntity<TKey>, IAggregateRoot
    {
        Task DeleteAsync(Guid id);
    }
}