using System.Threading.Tasks;
using Wimt.DomainDrivenArchitecture.Domain.Models;

namespace Wimt.DomainDrivenArchitecture.Domain.Repositories
{
    public interface IRepository<TDomainEntity, TKey>
        where TDomainEntity : DomainEntity<TKey>, IAggregateRoot
    {
        TDomainEntity Find(TKey id);
    }
}