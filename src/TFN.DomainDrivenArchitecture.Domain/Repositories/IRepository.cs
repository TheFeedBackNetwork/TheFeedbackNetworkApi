using System.Threading.Tasks;
using TFN.DomainDrivenArchitecture.Domain.Models;

namespace TFN.DomainDrivenArchitecture.Domain.Repositories
{
    public interface IRepository<TDomainEntity, TKey>
        where TDomainEntity : DomainEntity<TKey>, IAggregateRoot
    {
        Task<TDomainEntity> GetAsync(TKey id);
    }
}