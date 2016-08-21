using System.Threading.Tasks;
using Wimt.DomainDrivenArchitecture.Domain.Models;

namespace Wimt.DomainDrivenArchitecture.Domain.Repositories
{
    public interface IUpdateableRepository<TDomainEntity, TKey> : IRepository<TDomainEntity, TKey>
        where TDomainEntity : DomainEntity<TKey>, IAggregateRoot
    {
        Task UpdateAsync(TDomainEntity item);
    }
}