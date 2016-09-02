using System.Collections.Generic;
using System.Threading.Tasks;
using TFN.DomainDrivenArchitecture.Domain.Models;

namespace TFN.DomainDrivenArchitecture.Domain.Repositories
{
    public interface IAddableRepository<TDomainEntity, TKey> : IRepository<TDomainEntity, TKey>
       where TDomainEntity : DomainEntity<TKey>, IAggregateRoot
    {

        Task AddAsync(TDomainEntity item);

        Task AddBulkAsync(IEnumerable<TDomainEntity> items);
    }
}