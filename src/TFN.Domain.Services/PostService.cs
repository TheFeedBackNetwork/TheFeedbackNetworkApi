using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Interfaces.Services;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.ValueObjects;

namespace TFN.Domain.Services
{
    public class PostService : IPostService
    {
        public IPostRepository PostRepository { get; private set; }
        public ICommentRepository CommentRepository { get; private set; }

        public PostService(IPostRepository postRepository, ICommentRepository commentRepository)
        {
            PostRepository = postRepository;
            CommentRepository = commentRepository;
        }

        public async Task<CommentSummary> GetCommentScoreSummaryAsync(Guid commentId, int limit, string username)
        {
            return await CommentRepository.GetCommentScoreSummaryAsync(commentId, limit, username);
        }

        public async Task<PostSummary> GetPostLikeSummaryAsync(Guid postId, int limit, string username)
        {
            return await PostRepository.GetPostLikeSummaryAsync(postId, limit, username);
        }

        public async Task<IReadOnlyList<Post>> GetAllPostsAsync(int offset, int limit)
        {
            return await PostRepository.GetAllAsync(offset, limit);
        }

        public async Task<Post> GetPostAsync(Guid postId)
        {
            return await PostRepository.GetAsync(postId);
        }

        public async Task AddAsync(Post post)
        {
            await PostRepository.AddAsync(post);
        }

        public async Task UpdateAsync(Post entity)
        {
            await PostRepository.UpdateAsync(entity);
        }

        public async Task<IReadOnlyList<Like>> GetAllLikesAsync(Guid postId, int offset, int limit)
        {
            return await PostRepository.GetAllLikes(postId, offset, limit);
        }
        public async Task<Like> GetLikeAsync(Guid postId, Guid likeId)
        {
            return await PostRepository.GetLikeAsync(postId, likeId);
        }

        public async Task<IReadOnlyList<Comment>> GetCommentsAsync(Guid postId, int offset, int limit)
        {
            return await CommentRepository.GetCommentsAsync(postId, offset, limit);
        }

        public async Task<Comment> GetCommentAsync(Guid postId, Guid commentId)
        {
            //TODO Check for existing post or not?
            return await CommentRepository.GetAsync(commentId);
        }

        public async Task<IReadOnlyList<Comment>> GetAllCommentsAsync(Guid postId)
        {
            return await CommentRepository.GetAllCommentsAsync(postId);
        }

        public async Task<IReadOnlyList<Score>> GetAllScoresAsync(Guid commentId, int offset, int limit)
        {
            return await CommentRepository.GetAllScores(commentId, offset, limit);
        }

        public async Task<Score> GetScoreAsync(Guid commentId, Guid scoreId)
        {
            return await CommentRepository.GetAsync(commentId, scoreId);
        }

        public async Task UpdateAsync(Comment entity)
        {
            await CommentRepository.UpdateAsync(entity);
        }

        public async Task AddAsync(Comment entity)
        {
            await CommentRepository.AddAsync(entity);
        }

        public async Task AddAsync(Score entity)
        {
            await CommentRepository.AddAsync(entity);
        }

        public async Task DeletePostAsync(Guid postId)
        {
            await PostRepository.DeleteAsync(postId);
        }

        public async Task DeleteLikeAsync(Guid postId, Guid likeId)
        {
            await PostRepository.DeleteLike(postId, likeId);
        }

        public async Task DeleteCommentAsync(Guid commentId)
        {
            await CommentRepository.DeleteAsync(commentId);
        }

        public async Task DeleteScoreAsync(Guid commentId, Guid scoreId)
        {
            await CommentRepository.DeleteAsync(commentId, scoreId);
        }


    }
}