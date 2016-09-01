using TFN.DomainDrivenArchitecture.Domain.Models;

namespace TFN.DomainDrivenArchitecture.Domain.Repositories
{
    public interface IRepository<TDomainEntity, TKey>
        where TDomainEntity : DomainEntity<TKey>, IAggregateRoot
    {
        TDomainEntity FindAsync(TKey id);
    }
}