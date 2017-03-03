using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.ValueObjects;

namespace TFN.Domain.Interfaces.Services
{
    public interface IPostService
    {
        Task<CommentSummary>  GetCommentScoreSummaryAsync(Guid commentId, int limit, string username);
        Task<PostSummary> GetPostLikeSummaryAsync(Guid postId, int limit, string username);
        Task<IReadOnlyList<Post>> GetAllPostsAsync(int offset, int limit);
        Task<Post> GetPostAsync(Guid postId);
        Task AddAsync(Post post);
        Task UpdateAsync(Post entity);
        Task<IReadOnlyList<Like>> GetAllLikesAsync(Guid postId, int offset, int limit);
        Task<IReadOnlyList<Comment>> GetCommentsAsync(Guid postId, int offset, int limit);
        Task<Comment> GetCommentAsync(Guid postId, Guid commentId);
        Task<IReadOnlyList<Comment>> GetAllCommentsAsync(Guid postId);
        Task<IReadOnlyList<Score>> GetAllScoresAsync(Guid commentId, int offset, int limit);
        Task<Score> GetScoreAsync(Guid commentId, Guid scoreId);
        Task<Like> GetLikeAsync(Guid postId, Guid likeId);
        Task UpdateAsync(Comment entity);
        Task AddAsync(Comment entity);
        Task AddAsync(Score entity);
        Task DeletePostAsync(Guid postId);
        Task DeleteLikeAsync(Guid postId, Guid likeId);
        Task DeleteCommentAsync(Guid commentId);
        Task DeleteScoreAsync(Guid commentId, Guid scoreId);
    }
}