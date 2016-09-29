using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TFN.Domain.Models.Entities;
using TFN.DomainDrivenArchitecture.Domain.Repositories;

namespace TFN.Domain.Interfaces.Repositories
{
    public interface IPostRepository : IAddableRepository<Post,Guid> , IUpdateableRepository<Post,Guid>, IDeleteableRepository<Post,Guid>
    {
        Task<IReadOnlyList<Post>> GetAllAsync(int postOffset, int postLimit, int commentOffset,int commentLimit);
        Task<Post> GetAsync(Guid postId, int commentOffset, int commentLimit);
        Task<Comment> GetAsync(Guid postId, Guid commentId);
        Task<Score> GetAsync(Guid postId, Guid commentId,Guid scoreId);
        Task UpdateAsync(Comment entity);
        Task AddAsync(Comment entity);
        Task AddAsync(Score entity);
        Task DeleteAsync(Guid postId, Guid commentId);
        Task DeleteAsync(Guid postId, Guid commentId, Guid scoreId);
    }
}
