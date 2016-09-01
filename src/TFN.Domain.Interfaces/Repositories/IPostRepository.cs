

using System;
using TFN.Domain.Models.Entities;
using TFN.DomainDrivenArchitecture.Domain.Repositories;

namespace TFN.Domain.Interfaces.Repositories
{
    public interface IPostRepository : IRepository<Post,Guid>, IAddableRepository<Post,Guid> , IUpdateableRepository<Post,Guid>, IDeleteableRepository<Post,Guid>
    {
        
    }
}
