using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.ValueObjects;
using TFN.DomainDrivenArchitecture.Domain.Repositories;

namespace TFN.Domain.Interfaces.Repositories
{
    public interface IPostRepository : IAddableRepository<Post,Guid> , IUpdateableRepository<Post,Guid>, IDeleteableRepository<Post,Guid>
    {
        Task<IReadOnlyList<Post>> GetAllAsync(int offset, int limit);
        Task<PostSummary> GetPostLikeSummaryAsync(Guid postId, int limit, string username);
        Task<IReadOnlyList<Like>> GetAllLikes(Guid postId, int offset, int limit);
        Task DeleteLike(Guid postId, Guid likeId);
        Task<Like> GetLikeAsync(Guid postId, Guid likeId);
    }
}
