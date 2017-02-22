using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Models.Entities;
using TFN.Domain.Models.ValueObjects;

namespace TFN.Infrastructure.Repositories.PostAggregate.InMemory
{
    public class PostInMemoryRepository : IPostRepository
    {
        public  Task AddAsync(Post entity)
        {
            InMemoryPosts.Posts.Add(entity);

            return Task.CompletedTask;
        }

        public Task<IReadOnlyList<Post>> GetAllAsync(int offset, int limit)
        {
            IReadOnlyList<Post> posts = InMemoryPosts.Posts
                .Where(x => x.IsActive)
                .OrderBy(x => x.Created)
                .Skip(offset)
                .Take(limit)
                .ToList();

            return Task.FromResult(posts);
        }

        public Task<Post> GetAsync(Guid postId)
        {
            return Task.FromResult(InMemoryPosts.Posts.SingleOrDefault(x => x.Id == postId && x.IsActive));
        }

        public Task UpdateAsync(Post entity)
        {
            DeleteAsync(entity.Id);
            AddAsync(entity);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            InMemoryPosts.Posts.RemoveAll(x => x.Id == id);

            return Task.CompletedTask;
        }

        public Task<PostSummary> GetPostLikeSummaryAsync(Guid postId, int limit, string username)
        {
            var hasLiked = InMemoryLikes.Likes.Any(x => x.Username == username && x.PostId == postId);
            var count = InMemoryLikes.Likes.FindAll(x => x.PostId == postId).Count;
            IReadOnlyList<Like> someLikes = InMemoryLikes.Likes.FindAll(x => x.PostId == postId).Take(limit).ToList();

            var summary = PostSummary.From(postId,someLikes, count, hasLiked);

            return Task.FromResult(summary);
        }

        public Task<IReadOnlyList<Like>> GetAllLikes(Guid postId, int offset, int limit)
        {
            IReadOnlyList<Like> likes = InMemoryLikes.Likes.FindAll(x => x.PostId == postId)
                    .OrderBy(x => x.Created)
                    .Skip(offset)
                    .Take(limit)
                    .ToList();

            return Task.FromResult(likes);
        }

        public Task DeleteLike(Guid postId, Guid likeId)
        {
            InMemoryLikes.Likes.RemoveAll(x => x.PostId == postId && x.Id == likeId);

            return Task.CompletedTask;
        }

        public Task<Like> GetLikeAsync(Guid postId, Guid likeId)
        {
            var like = InMemoryLikes.Likes.SingleOrDefault(x => x.PostId == postId && x.Id == likeId);

            return Task.FromResult(like);
        }
    }
}
