using System.Collections.Generic;
using System.Threading.Tasks;
using Wimt.DomainDrivenArchitecture.Domain.Models;

namespace Wimt.DomainDrivenArchitecture.Domain.Repositories
{
    public interface IAddableRepository<TDomainEntity, TKey> : IRepository<TDomainEntity, TKey>
       where TDomainEntity : DomainEntity<TKey>, IAggregateRoot
    {
        void Add(TDomainEntity item);

        Task AddAsync(TDomainEntity item);

        void AddBulk(IEnumerable<TDomainEntity> items);

        Task AddBulkAsync(IEnumerable<TDomainEntity> items);
    }
}