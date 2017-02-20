using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TFN.Domain.Models.Entities;
using TFN.DomainDrivenArchitecture.Domain.Repositories;

namespace TFN.Domain.Interfaces.Repositories
{
    public interface ICreditRepository : IAddableRepository<Credits, Guid>, IUpdateableRepository<Credits, Guid>
    {
        Task<Credits> GetByUsername(string username);
        Task<Credits> GetByUserId(Guid userId);
        Task<IReadOnlyList<Credits>> GetHighestCredits(int offset, int limit);
        Task<IReadOnlyList<Credits>> SearchUsers(string searchToken, int offset, int limit);
    }
}