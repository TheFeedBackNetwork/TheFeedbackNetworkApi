using System;
using System.Threading.Tasks;
using TFN.Domain.Models.Entities;
using TFN.DomainDrivenArchitecture.Domain.Repositories;

namespace TFN.Domain.Interfaces.Repositories
{
    public interface ICreditRepository : IAddableRepository<Credit, Guid>, IUpdateableRepository<Credit, Guid>
    {
        Task<Credit> GetByUsername(string username);
    }
}