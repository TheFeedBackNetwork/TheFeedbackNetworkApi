using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Models.Entities;

namespace TFN.Infrastructure.Repositories.PostAggregate.InMemory
{
    public class PostInMemoryRepository : IPostRepository
    {
        public  Task AddAsync(Post entity)
        {
            InMemoryPosts.Posts.Add(entity);
            return Task.FromResult(0);
        }

        public Task AddAsync(Comment entity)
        {
            InMemoryPosts.Posts.Single(x => x.Id == entity.PostId).Comments.Add(entity);
            return Task.CompletedTask;
        }

        public Task AddAsync(Score entity)
        {
            foreach (var post in InMemoryPosts.Posts)
            {
                if (post.Comments.Any(x => x.Id == entity.CommentId))
                {
                    post.Comments.Single(x => x.Id == entity.CommentId).Scores.Add(entity);
                }
            }
            return Task.CompletedTask;
        }




        public Task<IReadOnlyList<Post>> GetAllAsync(int postOffset, int postLimit, int commentOffset, int commentLimit)
        {
            IReadOnlyList<Post> posts = InMemoryPosts.Posts.Skip(postOffset).Take(postLimit).Where(x => x.IsActive).ToList();
            return Task.FromResult(posts);
        }

        public Task<Post> GetAsync(Guid postId)
        {
            return Task.FromResult(InMemoryPosts.Posts.SingleOrDefault(x => x.Id == postId && x.IsActive));
        }

        public Task<Post> GetAsync(Guid postId, int commentOffset, int commentLimit)
        {
            return Task.FromResult(InMemoryPosts.Posts.SingleOrDefault(x => x.Id == postId));
        }

        public Task<Comment> GetAsync(Guid postId, Guid commentId)
        {
            var comment =
                InMemoryPosts.Posts.SingleOrDefault(x => x.Id == postId)?
                    .Comments.SingleOrDefault(x => x.Id == commentId);
            return Task.FromResult(comment);
        }

        public Task<Score> GetAsync(Guid postId, Guid commentId, Guid scoreId)
        {
            var score =
                InMemoryPosts.Posts.SingleOrDefault(x => x.Id == postId)?
                    .Comments.SingleOrDefault(x => x.Id == commentId)?
                    .Scores.SingleOrDefault(z => z.Id == scoreId);
            return Task.FromResult(score);
        }

        public async Task UpdateAsync(Comment entity)
        {
            var post = await GetAsync(entity.PostId);

            post.EditComment(entity);
        }
        public Task UpdateAsync(Post entity)
        {
            DeleteAsync(entity.Id);
            AddAsync(entity);
            return Task.FromResult(0);
        }

        public Task DeleteAsync(Guid id)
        {
            InMemoryPosts.Posts.RemoveAll(x => x.Id == id);
            return Task.FromResult(0);
        }


        public Task DeleteAsync(Guid postId, Guid commentId)
        {
            foreach (var post in InMemoryPosts.Posts)
            {
                var comment = post.Comments.SingleOrDefault(x => x.Id == commentId);
                post.Comments.Remove(comment);
            }
            return Task.FromResult(0);
        }

        public Task DeleteAsync(Guid postId, Guid commentId, Guid scoreId)
        {
            foreach (var post in InMemoryPosts.Posts)
            {
                foreach (var comment in post.Comments)
                {
                    var score = comment.Scores.SingleOrDefault(x => x.Id == scoreId);
                    comment.Scores.Remove(score);
                }
                
            }
            return Task.FromResult(0);
        }

        
    }
}
